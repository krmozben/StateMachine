using Boyner.Invoice;
using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using MassTransit.Mediator;
using Polly.Retry;
using ReturnOrderService.Configuration;
using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Mp.Status.Base;
using System.Text.Json;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpPoSuccess : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpPoSuccess> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MpPoSuccess(MpReturnOrderStateMachine stateMachine, ILogger<MpPoSuccess> logger, RabbitMqConfiguration rabbitMqConfiguration, ISendEndpointProvider sendEndpointProvider)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public override async void Handle()
        {
            InvoiceDO invoice = (InvoiceDO)_stateMachine.Model.InvoiceDO;

            EntegratorReturnOrderRequestEvent model = new EntegratorReturnOrderRequestEvent()
            {
                Platform = _stateMachine.Model.Platform,
                SapOrderCode = invoice.OrderCode,
                MpOrderCode = _stateMachine.Model.MPOrderNumber,
                Lines = invoice.ItemList.Select(x => new ReturnOrderLine() { Quantity = 1, Sku = x.ProductCode })
                      .ToList()
            };

            _logger.LogWithCorrelationId(LogLevel.Information, _stateMachine.Model.CorrelationId, $"İade Kabulu için Entegratorün dinlediği kuyruğa bırakılması için hazırlandı:\r\n { JsonSerializer.Serialize(model, LogHelper.JsonSerializerOptions)}");

            try
            {
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{_rabbitMqConfiguration.Queues.ReturnOrderToEntegratorQueue}"));

                await endpoint.Send(model);

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpSendEntegratorSuccess>());
            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, _stateMachine.Model.CorrelationId, ex, $"İade Kabulu için Entegratorün dinlediği kuyruğa bırakılamadı:\r\n {JsonSerializer.Serialize(new { model, ex }, LogHelper.JsonSerializerOptions)}");

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpSendEntegratorFail>());
            }
        }
    }
}
