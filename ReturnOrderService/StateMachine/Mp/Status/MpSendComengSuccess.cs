using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Mp.Status.Base;
using System.Text.Json;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpSendComengSuccess : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpSendComengSuccess> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MpSendComengSuccess(MpReturnOrderStateMachine stateMachine, ILogger<MpSendComengSuccess> logger, RabbitMqConfiguration rabbitMqConfiguration, ISendEndpointProvider sendEndpointProvider)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public override void Handle()
        {
            try
            {
                var endpoint = _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{_rabbitMqConfiguration.Queues.CreateReturnInvoiceToMpQueue}")).Result;
                endpoint.Send(_stateMachine.Model);

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpSendReturnInvoiceToMpSuccess>());
            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, _stateMachine.Model.CorrelationId, ex, JsonSerializer.Serialize(_stateMachine.Model.CorrelationId, LogHelper.JsonSerializerOptions));

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpSendReturnInvoiceToMpFail>());
            }
        }
    }
}
