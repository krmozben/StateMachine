using ReturnOrderService.Models.Requests;
using ReturnOrderService.Models.Responses;

namespace ReturnOrderService.Services.Interfaces
{
    public interface IChannelService
    {
        Task<bool> ReceiveSuccessEkolRequestHandler(ReceiveSuccessEkolRequestModel request);
        Task<BaseResponse<HttpResponseMessage>> ReturnOrderSendGksRequestHandler(BoynerReturnOrderRequestModel request);
        Task<BaseResponse<bool>> SendReturnOrderInvoiceRequestHandler(MpReturnOrderRequestModel request);
    }
}
