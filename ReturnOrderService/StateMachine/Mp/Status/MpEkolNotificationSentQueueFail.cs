using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpEkolNotificationSentQueueFail: MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;

        public MpEkolNotificationSentQueueFail(MpReturnOrderStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Handle() => _stateMachine.StateFinalize();
    }
}
