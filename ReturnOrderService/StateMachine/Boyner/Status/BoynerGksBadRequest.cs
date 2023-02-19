using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerGksBadRequest : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;

        public BoynerGksBadRequest(BoynerReturnOrderStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Handle() => _stateMachine.StateFinalize();
    }
}
