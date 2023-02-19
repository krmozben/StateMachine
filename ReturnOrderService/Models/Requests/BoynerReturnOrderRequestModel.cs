using DigitalSolutions.COMS.Contracts.Events.MarketPlace;
using MediatR;
using ReturnOrderService.Models.Responses;

namespace ReturnOrderService.Models.Requests
{
    public class BoynerReturnOrderRequestModel : IRequest<BaseResponse<HttpResponseMessage>>
    {
        public BoynerReturnOrderRequestEvent RequestEvent { get; set; }
    }
}
