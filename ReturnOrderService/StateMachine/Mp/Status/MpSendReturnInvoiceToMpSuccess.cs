using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Mp.Status.Base;
using System.Text.Json;


namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpSendReturnInvoiceToMpSuccess : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpSendReturnInvoiceToMpSuccess> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MpSendReturnInvoiceToMpSuccess(MpReturnOrderStateMachine stateMachine, ILogger<MpSendReturnInvoiceToMpSuccess> logger, RabbitMqConfiguration rabbitMqConfiguration, ISendEndpointProvider sendEndpointProvider)
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
                var receiveSuccessRequest = new ReceiveSuccessEkolRequestEvent()
                {
                    OrgCode = _stateMachine.Model.OrganizationCode,
                    RecordNo = _stateMachine.Model.RecordNo,
                    CorrelationId = _stateMachine.Model.CorrelationId
                };

                var endpoint = _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{_rabbitMqConfiguration.Queues.ReceiveSuccessEkolQueue}")).Result;
                endpoint.Send(receiveSuccessRequest);

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpEkolNotificationSentQueueSuccess>());
            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, _stateMachine.Model.CorrelationId, ex, JsonSerializer.Serialize(_stateMachine.Model.CorrelationId, LogHelper.JsonSerializerOptions));

                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpEkolNotificationSentQueueFail>());
            }
        }
    }
}
