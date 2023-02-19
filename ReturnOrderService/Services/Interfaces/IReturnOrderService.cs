using ReturnOrderService.Models.Domain;
using ReturnOrderService.Models.Responses;

namespace ReturnOrderService.Services.Interfaces
{
    public interface IReturnOrderService
    {
        Task InsertReturnOrder(ReturnOrderModel model);
        Task<BaseResponse<ReturnOrderModel>> GetLastStateByCorrelationId(Guid correlationId);
        Task<BaseResponse<ReturnOrderModel>> GetLastStateByTraceCode(string traceCode);
    }
}
