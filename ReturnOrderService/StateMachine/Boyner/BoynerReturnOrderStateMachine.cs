using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using ReturnOrderService.Configuration.Constants;
using ReturnOrderService.Models.Domain;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Boyner.Status;
using ReturnOrderService.StateMachine.Boyner.Status.Base;
using System.Text.Json;

namespace ReturnOrderService.StateMachine.Boyner
{
    public class BoynerReturnOrderStateMachine
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReturnOrderService _returnOrderService;
        public BoynerReturnOrderStateMachine(IServiceProvider serviceProvider, IReturnOrderService returnOrderService)
        {
            _serviceProvider = serviceProvider;
            _returnOrderService = returnOrderService;
        }

        private BoynerReturnOrderState _state;
        public BoynerReturnOrderState State
        {
            get { return _state; }
            private set
            {
                _state = value;

                if (value == null)
                    return;

                SetEventSource();

                _state.Handle();
            }
        }

        private BoynerReturnOrderRequestEvent _model;
        public BoynerReturnOrderRequestEvent Model
        {
            get { return _model; }
            private set
            {
                _model = value;
                State = _state ?? CreateStateInstance<BoynerInitial>();
            }
        }

        public void SetModelAndStartProcess(BoynerReturnOrderRequestEvent model, BoynerReturnOrderState state = null)
        {
            if (state != null)
                _state = state;

            Model = model;
        }

        public void SetState(BoynerReturnOrderState state) => State = state;
        public T CreateStateInstance<T>() where T : BoynerReturnOrderState => _serviceProvider.GetRequiredService<T>();
        public void StateFinalize() => State = CreateStateInstance<BoynerFinalized>();
        private void SetEventSource()
        {
            ReturnOrderModel returnOrderModel = new ReturnOrderModel
            {
                CorrelationId = Model.CorrelationId,
                CreatedAt = DateTime.UtcNow,
                ReturnType = ReturnType.Boyner,
                State = State.GetType().Name,
                Model = JsonSerializer.Serialize<BoynerReturnOrderRequestEvent>(Model),
                TraceCode = Model.TraceCode
            };

            _returnOrderService.InsertReturnOrder(returnOrderModel);
        }
    }
}
