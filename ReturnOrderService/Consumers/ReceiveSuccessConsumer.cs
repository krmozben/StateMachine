using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MassTransit;
using ReturnOrderService.Configuration.Constants;
using ReturnOrderService.Helper;
using ReturnOrderService.Models.Requests;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Boyner;
using ReturnOrderService.StateMachine.Boyner.Status;
using ReturnOrderService.StateMachine.Boyner.Status.Base;
using ReturnOrderService.StateMachine.Mp;
using ReturnOrderService.StateMachine.Mp.Status;
using ReturnOrderService.StateMachine.Mp.Status.Base;
using System.Text.Json;

namespace ReturnOrderService.Consumers
{
    public class ReceiveSuccessConsumer : IConsumer<ReceiveSuccessEkolRequestEvent>
    {
        private readonly ILogger<ReceiveSuccessConsumer> _logger;
        private readonly BoynerReturnOrderStateMachine _boynerStateMachine;
        private readonly MpReturnOrderStateMachine _mpStateMachine;
        private readonly IReturnOrderService _returnOrderService;
        private readonly IChannelService _channelService;

        public ReceiveSuccessConsumer(ILogger<ReceiveSuccessConsumer> logger, BoynerReturnOrderStateMachine boynerStateMachine, MpReturnOrderStateMachine mpStateMachine, IReturnOrderService returnOrderService, IChannelService channelService)
        {
            _logger = logger;
            _boynerStateMachine = boynerStateMachine;
            _mpStateMachine = mpStateMachine;
            _returnOrderService = returnOrderService;
            _channelService = channelService;
        }

        public async Task Consume(ConsumeContext<ReceiveSuccessEkolRequestEvent> context)
        {
            var existStateReturnOrder = await _returnOrderService.GetLastStateByCorrelationId(context.Message.CorrelationId);

            if (existStateReturnOrder.Data == null)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, context.Message.CorrelationId, "ReturnOrderModel was not found at Mongo");
                return;
            }

            if (existStateReturnOrder.Data.ReturnType == ReturnType.Boyner)
            {
                BoynerReturnOrderRequestEvent model = JsonSerializer.Deserialize<BoynerReturnOrderRequestEvent>(existStateReturnOrder.Data.Model);
                BoynerReturnOrderState state = null;

                if (await _channelService.ReceiveSuccessEkolRequestHandler(new ReceiveSuccessEkolRequestModel() { RequestEvent = context.Message }))
                    state = _boynerStateMachine.CreateStateInstance<BoynerEkolNotificationSuccess>();
                else
                    state = _boynerStateMachine.CreateStateInstance<BoynerEkolNotificationFail>();

                _boynerStateMachine.SetModelAndStartProcess(model, state);
            }
            else
            {
                MpReturnOrderRequestEvent model = JsonSerializer.Deserialize<MpReturnOrderRequestEvent>(existStateReturnOrder.Data.Model);
                MpReturnOrderState state = null;

                if (await _channelService.ReceiveSuccessEkolRequestHandler(new ReceiveSuccessEkolRequestModel() { RequestEvent = context.Message }))
                    state = _mpStateMachine.CreateStateInstance<MpEkolNotificationSuccess>();
                else
                    state = _mpStateMachine.CreateStateInstance<MpEkolNotificationFail>();

                _mpStateMachine.SetModelAndStartProcess(model, state);
            }
        }
    }
}
