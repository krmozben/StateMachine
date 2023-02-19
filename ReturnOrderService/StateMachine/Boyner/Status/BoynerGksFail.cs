using MassTransit;
using ReturnOrderService.Configuration;
using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerGksFail : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly ILogger<BoynerGksFail> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IConfiguration _configuration;
        private readonly IMessageScheduler _messageScheduler;

        public BoynerGksFail(BoynerReturnOrderStateMachine stateMachine, IConfiguration configuration, ILogger<BoynerGksFail> logger, IMessageScheduler messageScheduler)
        {
            _stateMachine = stateMachine;
            _configuration = configuration;
            _logger = logger;
            _messageScheduler = messageScheduler;

            _rabbitMqConfiguration = _configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();
        }

        public override void Handle()
        {
            /// GKS ye gönderilemeyen kayıt tekrar queue ya bırakılır.
            /// Burada geçikmeli gönderme yapılmasının sebei her hatada sürekli döngü halinde olacaktır.

            _messageScheduler.ScheduleSend(new Uri($"queue:{_rabbitMqConfiguration.Queues.ReturnOrderBoynerQueue}"), DateTime.UtcNow + TimeSpan.FromMinutes(5), _stateMachine.Model);
        }
    }
}
