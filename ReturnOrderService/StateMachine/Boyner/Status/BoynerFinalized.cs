using ReturnOrderService.Helper;
using ReturnOrderService.StateMachine.Boyner.Status.Base;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerFinalized : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly ILogger<BoynerFinalized> _logger;

        public BoynerFinalized(BoynerReturnOrderStateMachine stateMachine, ILogger<BoynerFinalized> logger)
        {
            _stateMachine = stateMachine;
            _logger = logger;
        }

        public override void Handle() => _logger.LogWithCorrelationId(LogLevel.Information, _stateMachine.Model.CorrelationId, $" Boyner Return Order state machine is finalized");
    }
}
