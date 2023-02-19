using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Mp;
using ReturnOrderService.StateMachine.Mp.Status;

namespace ReturnOrderService.Consumers.MarketPlace
{
    public class MpReturnOrderConsumer : IConsumer<MpReturnOrderRequestEvent>
    {
        private readonly ILogger<MpReturnOrderConsumer> _logger;
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly IReturnOrderService _returnOrderService;

        public MpReturnOrderConsumer(ILogger<MpReturnOrderConsumer> logger, MpReturnOrderStateMachine stateMachine, IReturnOrderService returnOrderService)
        {
            _logger = logger;
            _stateMachine = stateMachine;
            _returnOrderService = returnOrderService;
        }

        public async Task Consume(ConsumeContext<MpReturnOrderRequestEvent> context)
        {
            // Burada aynı veri gelmesi halinde önceki gelen verinin işlemde olup olmadığı kontrol edilir. Veri Finalized durumuna geçtiğinde tekrar akışa dahil edilebilir.

            var result = await _returnOrderService.GetLastStateByTraceCode(context.Message.TraceCode);

            if (result.Data != null)
                if (result.Data.CorrelationId != context.CorrelationId)
                    if (result.Data.State != nameof(MpFinalized))
                        return;

            _stateMachine.SetModelAndStartProcess(context.Message);
        }
    }
}
