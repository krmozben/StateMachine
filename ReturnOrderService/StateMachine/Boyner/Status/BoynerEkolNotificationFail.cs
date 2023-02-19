using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerEkolNotificationFail : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly ILogger<BoynerEkolNotificationFail> _logger;
        private readonly IMessageScheduler _messageScheduler;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IConfiguration _configuration;

        public BoynerEkolNotificationFail(BoynerReturnOrderStateMachine stateMachine, IMessageScheduler messageScheduler, IConfiguration configuration, ILogger<BoynerEkolNotificationFail> logger)
        {
            _stateMachine = stateMachine;
            _messageScheduler = messageScheduler;
            _configuration = configuration;
            _logger = logger;

            _rabbitMqConfiguration = _configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();
        }

        public override void Handle()
        {
            /// Ekol bilgilendirme için gönderilemeyen kayıt tekrar queue ya bırakılır. 
            /// Burada geçikmeli gönderme yapılmalı yoksa her hatasında sürekli döngü halinde olacaktır.

            var receiveSuccessRequest = new ReceiveSuccessEkolRequestEvent()
            {
                OrgCode = _stateMachine.Model.OrganizationCode,
                RecordNo = _stateMachine.Model.RecordNo,
                CorrelationId = _stateMachine.Model.CorrelationId
            };

            _messageScheduler.ScheduleSend(new Uri($"queue:{_rabbitMqConfiguration.Queues.ReceiveSuccessEkolQueue}"), DateTime.UtcNow + TimeSpan.FromMinutes(5), receiveSuccessRequest);
        }
    }
}
