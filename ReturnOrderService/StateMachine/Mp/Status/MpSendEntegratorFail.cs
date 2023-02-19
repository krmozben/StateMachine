using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpSendEntegratorFail : MpReturnOrderState
    {

        private readonly MpReturnOrderStateMachine _stateMachine;

        public MpSendEntegratorFail(MpReturnOrderStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Handle() => _stateMachine.StateFinalize();
    }
}