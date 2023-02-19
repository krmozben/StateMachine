using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpSendComengFail : MpReturnOrderState
    {

        private readonly MpReturnOrderStateMachine _stateMachine;

        public MpSendComengFail(MpReturnOrderStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Handle() => _stateMachine.StateFinalize();
    }
}