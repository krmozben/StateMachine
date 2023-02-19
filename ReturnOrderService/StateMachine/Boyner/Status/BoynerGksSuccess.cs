using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerGksSuccess : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly ILogger<BoynerGksSuccess> _logger;
        private readonly IBusControl _busControl;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IConfiguration _configuration;

        public BoynerGksSuccess(BoynerReturnOrderStateMachine stateMachine, IBusControl busControl, IConfiguration configuration, ILogger<BoynerGksSuccess> logger)
        {
            _stateMachine = stateMachine;
            _busControl = busControl;
            _configuration = configuration;
            _logger = logger;

            _rabbitMqConfiguration = _configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();
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

                var endpoint = _busControl.GetSendEndpoint(new Uri($"queue:{_rabbitMqConfiguration.Queues.ReceiveSuccessEkolQueue}")).Result;
                endpoint.Send(receiveSuccessRequest);

                _stateMachine.SetState(_stateMachine.CreateStateInstance<BoynerEkolNotificationSentQueueSuccess>());
            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, _stateMachine.Model.CorrelationId, ex, ex.Message);

                _stateMachine.SetState(_stateMachine.CreateStateInstance<BoynerEkolNotificationSentQueueFail>());
            }
        }
    }
}
