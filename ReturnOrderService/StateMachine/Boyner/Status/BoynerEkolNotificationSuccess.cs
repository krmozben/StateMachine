using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerEkolNotificationSuccess : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        public BoynerEkolNotificationSuccess(BoynerReturnOrderStateMachine stateMachine) => _stateMachine = stateMachine;

        public override void Handle() => _stateMachine.StateFinalize();
    }
}
