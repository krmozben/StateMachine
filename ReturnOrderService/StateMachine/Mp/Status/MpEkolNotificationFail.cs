using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpEkolNotificationFail : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpEkolNotificationFail> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IMessageScheduler _messageScheduler;

        public MpEkolNotificationFail(MpReturnOrderStateMachine stateMachine, ILogger<MpEkolNotificationFail> logger, RabbitMqConfiguration rabbitMqConfiguration, IMessageScheduler messageScheduler)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _messageScheduler = messageScheduler;
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
