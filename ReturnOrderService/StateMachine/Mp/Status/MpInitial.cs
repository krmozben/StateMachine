using ReturnOrderService.Helper;
using ReturnOrderService.Models.Requests;
using ReturnOrderService.Models.Responses;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Mp.Status.Base;
using System.Text.Json;

namespace ReturnOrderService.StateMachine.Mp.Status
{
    public class MpInitial : MpReturnOrderState
    {
        private readonly MpReturnOrderStateMachine _stateMachine;
        private readonly ILogger<MpInitial> _logger;
        private readonly IChannelService _channelService;

        public MpInitial(MpReturnOrderStateMachine stateMachine, ILogger<MpInitial> logger, IChannelService channelService)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            _channelService = channelService;
        }

        public override void Handle()
        {
            BaseResponse<bool> response = _channelService.SendReturnOrderInvoiceRequestHandler(new MpReturnOrderRequestModel() { RequestEvent = _stateMachine.Model }).Result;

            if (!response.HasError)
            {
                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpPoSuccess>());
            }
            else
            {
                _logger.LogWithCorrelationId(LogLevel.Error, _stateMachine.Model.CorrelationId, JsonSerializer.Serialize(response.Errors, LogHelper.JsonSerializerOptions));
                _stateMachine.SetState(_stateMachine.CreateStateInstance<MpPoFail>());
            }
        }
    }
}
