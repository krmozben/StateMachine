using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerEkolNotificationSentQueueFail : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly ILogger<BoynerEkolNotificationSentQueueFail> _logger;

        public BoynerEkolNotificationSentQueueFail(BoynerReturnOrderStateMachine stateMachine, ILogger<BoynerEkolNotificationSentQueueFail> logger)
        {
            _stateMachine = stateMachine;
            _logger = logger;
        }

        public override void Handle() => _stateMachine.StateFinalize();
    }
 
}
