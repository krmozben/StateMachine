using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpPoFail : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpPoFail> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IMessageScheduler _messageScheduler;

        public MpPoFail(MpReturnOrderStateMachine stateMachine, ILogger<MpPoFail> logger, RabbitMqConfiguration rabbitMqConfiguration, IMessageScheduler messageScheduler)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _messageScheduler = messageScheduler;
        }

        public override void Handle()
        {
            /// PO ya gönderilemeyen kayıt tekrar queue ya bırakılır.
            /// Burada geçikmeli gönderme yapılmasının sebei her hatada sürekli döngü halinde olacaktır.

            _messageScheduler.ScheduleSend(new Uri($"queue:{_rabbitMqConfiguration.Queues.ReturnOrderMpQueue}"), DateTime.UtcNow + TimeSpan.FromMinutes(5), _stateMachine.Model);
        }
    }
}