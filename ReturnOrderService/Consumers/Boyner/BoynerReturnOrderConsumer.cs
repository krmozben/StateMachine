using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Boyner;
using ReturnOrderService.StateMachine.Boyner.Status;

namespace ReturnOrderService.Consumers.Boyner
{
    public class BoynerReturnOrderConsumer : IConsumer<BoynerReturnOrderRequestEvent>
    {
        private readonly ILogger<BoynerReturnOrderConsumer> _logger;
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly IReturnOrderService _returnOrderService;

        public BoynerReturnOrderConsumer(ILogger<BoynerReturnOrderConsumer> logger, BoynerReturnOrderStateMachine stateMachine, IReturnOrderService returnOrderService)
        {
            _logger = logger;
            _stateMachine = stateMachine;
            _returnOrderService = returnOrderService;
        }

        public async Task Consume(ConsumeContext<BoynerReturnOrderRequestEvent> context)
        {
            // Burada aynı veri gelmesi halinde önceki gelen verinin işlemde olup olmadığı kontrol edilir. Veri Finalized durumuna geçtiğinde tekrar akışa dahil edilebilir.

            var result = await _returnOrderService.GetLastStateByTraceCode(context.Message.TraceCode);

            if (result.Data != null)
                if (result.Data.CorrelationId != context.CorrelationId)
                    if (result.Data.State != nameof(BoynerFinalized))
                        return;

            _stateMachine.SetModelAndStartProcess(context.Message);
        }
    }
}
