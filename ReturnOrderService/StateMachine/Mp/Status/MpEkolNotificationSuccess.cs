using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpEkolNotificationSuccess : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        public MpEkolNotificationSuccess(MpReturnOrderStateMachine stateMachine) => _stateMachine = stateMachine;

        public override void Handle() => _stateMachine.StateFinalize();
    }
}
