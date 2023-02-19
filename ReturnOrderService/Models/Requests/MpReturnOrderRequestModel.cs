using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MediatR;
using ReturnOrderService.Models.Responses;

namespace ReturnOrderService.Models.Requests
{
    public class MpReturnOrderRequestModel : IRequest<BaseResponse<bool>>
    {
        public MpReturnOrderRequestEvent RequestEvent { get; set; }
    }
}
