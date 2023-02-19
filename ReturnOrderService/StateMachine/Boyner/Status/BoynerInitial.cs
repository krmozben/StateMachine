using MediatR;
using ReturnOrderService.Helper;
using ReturnOrderService.Models.Requests;
using ReturnOrderService.Models.Responses;
using ReturnOrderService.Services.Interfaces;
using ReturnOrderService.StateMachine.Boyner.Status.Base;
using System.Net;

namespace ReturnOrderService.StateMachine.Boyner.Status
{
    public class BoynerInitial : BoynerReturnOrderState
    {
        private readonly BoynerReturnOrderStateMachine _stateMachine;
        private readonly ILogger<BoynerInitial> _logger;
        private readonly IChannelService _channelService;

        public BoynerInitial(BoynerReturnOrderStateMachine stateMachine, ILogger<BoynerInitial> logger, IChannelService channelService)
        {
            _stateMachine = stateMachine;
            _logger = logger;
            _channelService = channelService;
        }

        public override void Handle()
        {
            BaseResponse<HttpResponseMessage> response = new BaseResponse<HttpResponseMessage>();
            try
            {
                response = _channelService.ReturnOrderSendGksRequestHandler(new BoynerReturnOrderRequestModel() { RequestEvent = _stateMachine.Model }).Result;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                _logger.LogWithCorrelationId(LogLevel.Error, _stateMachine.Model.CorrelationId, ex, ex.Message);
                _stateMachine.SetState(_stateMachine.CreateStateInstance<BoynerGksFail>());
            }

            if (!response.HasError)
            {
                if (response.Data.StatusCode == HttpStatusCode.BadRequest)
                    _stateMachine.SetState(_stateMachine.CreateStateInstance<BoynerGksBadRequest>());

                if (response.Data.StatusCode == HttpStatusCode.OK || response.Data.StatusCode == HttpStatusCode.Accepted)
                    _stateMachine.SetState(_stateMachine.CreateStateInstance<BoynerGksSuccess>());
            }
        }
    }
}
