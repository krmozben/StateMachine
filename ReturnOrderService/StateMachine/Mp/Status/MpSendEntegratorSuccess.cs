using Boyner.Invoice;
using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpSendEntegratorSuccess : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpSendEntegratorSuccess> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MpSendEntegratorSuccess(MpReturnOrderStateMachine stateMachine, ILogger<MpSendEntegratorSuccess> logger, RabbitMqConfiguration rabbitMqConfiguration, ISendEndpointProvider sendEndpointProvider)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public override async void Handle()
        {
            var invoice = (InvoiceDO)_stateMachine.Model.InvoiceDO;

            var comengInvoice = new ComengInvoiceCreateRequestEvent()
            {
                OrderCode = invoice.OrderCode,
                Body = new Body
                {
                    Items = invoice.ItemList.Select(x => x.ID).ToList(),
                    InvoiceID = invoice.ID,
                    InvoiceNo = invoice.InvoiceNumber,
                    InvoiceRefCode = invoice.InvoiceRefCode,
                    InvoiceType = "-1",
                    InvoiceDate = invoice.InvoiceDate.AddHours(3), //utc+3 gönderiyoruz.
                    PackagingLocationCode = invoice.PackagingLocationCode,
                    IsHasShipmentPayment = invoice.ShipmentTotal != 0,
                    BonusResidual = null,
                    InvoiceStoreCode = null,
                }
            };

            _logger.LogWithCorrelationId(LogLevel.Information, _stateMachine.Model.CorrelationId, "İade Fatura Bilgisi Comengin dinlediği kuyruğa bırakılması için hazırlandı:\r\n {JsonSerializer.Serialize(comengInvoice, LogHelper.JsonSerializerOptions)}", LogHelper.JsonSerializerOptions);

            try
            {
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{_rabbitMqConfiguration.Queues.CreateInvoiceToComengQueue}"));

                await endpoint.Send(comengInvoice);

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpSendComengSuccess>());
            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, _stateMachine.Model.CorrelationId, ex, "İade Fatura Bilgisi Comenging dinlediği kuyruğa bırakılamadı:\r\n {JsonSerializer.Serialize(new { comengInvoice, ex }, LogHelper.JsonSerializerOptions)}");

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpSendComengFail>());
            }
        }
    }
}
