using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerEkolNotificationSentQueueSuccess : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly ILogger<BoynerEkolNotificationSentQueueSuccess> _logger;

        public BoynerEkolNotificationSentQueueSuccess(BoynerReturnOrderStateMachine stateMachine, ILogger<BoynerEkolNotificationSentQueueSuccess> logger)
        {
            _stateMachine = stateMachine;
            _logger = logger;
        }

        public override void Handle() => _logger.LogWithCorrelationId(LogLevel.Information, _stateMachine.Model.CorrelationId, $"Boyner Return Order sent to ekol queue");
    } 
}
