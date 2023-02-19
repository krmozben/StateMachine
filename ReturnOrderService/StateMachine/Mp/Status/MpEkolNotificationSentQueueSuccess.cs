using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Mp.Status.Base;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpEkolNotificationSentQueueSuccess : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpEkolNotificationSentQueueSuccess> _logger;

        public MpEkolNotificationSentQueueSuccess(MpReturnOrderStateMachine stateMachine, ILogger<MpEkolNotificationSentQueueSuccess> logger)
        {
            _stateMachine = stateMachine;
            _logger = logger;
        }

        public override void Handle() => _logger.LogWithCorrelationId(LogLevel.Information, _stateMachine.Model.CorrelationId, "Mp Return Order sent to ekol queue");
    }
}
