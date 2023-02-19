using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Mp.Status.Base;


namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpFinalized : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpFinalized> _logger;

        public MpFinalized(MpReturnOrderStateMachine stateMachine, ILogger<MpFinalized> logger)
        {
            _stateMachine = stateMachine;
            _logger = logger;
        }

        public override void Handle() => _logger.LogWithCorrelationId(LogLevel.Information, _stateMachine.Model.CorrelationId, "Mp Return Order state machine is finalized");
    }
}
