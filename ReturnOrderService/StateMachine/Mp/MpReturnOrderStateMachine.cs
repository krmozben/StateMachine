using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using ReturnOrderService.Configuration.Constants;
using ReturnOrderService.Models.Domain;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Mp.Status;
using ReturnOrderService.StateMachine.Mp.Status.Base;
using System.Text.Json;

namespace ReturnOrderService.StateMachine.Mp
{
    public class MpReturnOrderStateMachine
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReturnOrderService _returnOrderService;
        public MpReturnOrderStateMachine(IServiceProvider serviceProvider, IReturnOrderService returnOrderService)
        {
            _serviceProvider = serviceProvider;
            _returnOrderService = returnOrderService;
        }

        private MpReturnOrderState _state;
        public MpReturnOrderState State
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

        private MpReturnOrderRequestEvent _model;
        public MpReturnOrderRequestEvent Model
        {
            get { return _model; }
            private set
            {
                _model = value;
                State = _state ?? CreateStateInstance<MpInitial>();
            }
        }

        public void SetModelAndStartProcess(MpReturnOrderRequestEvent model, MpReturnOrderState state = null)
        {
            if (state != null)
                _state = state;

            Model = model;
        }

        public void SetState(MpReturnOrderState state) => State = state;
        public T CreateStateInstance<T>() where T : MpReturnOrderState => _serviceProvider.GetRequiredService<T>();
        public void StateFinalize() => State = CreateStateInstance<MpFinalized>();
        private void SetEventSource()
        {
            ReturnOrderModel returnOrderModel = new ReturnOrderModel
            {
                CorrelationId = Model.CorrelationId,
                CreatedAt = DateTime.UtcNow,
                ReturnType = ReturnType.Mp,
                State = State.GetType().Name,
                Model = JsonSerializer.Serialize<MpReturnOrderRequestEvent>(Model),
                TraceCode = Model.TraceCode
            };

            _returnOrderService.InsertReturnOrder(returnOrderModel);
        }
    }
}
