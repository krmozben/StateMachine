using MongoDB.Driver;
using Polly.Retry;
using ReturnOrderService.Data.Interfaces;
using ReturnOrderService.Helper;
using ReturnOrderService.Models.Domain;
using ReturnOrderService.Models.Responses;
using ReturnOrderService.Services.Interfaces;

namespace ReturnOrderService.Services
{
    public class ReturnOrderService : IReturnOrderService
    {
        private readonly ILogger<ReturnOrderService> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly IMongoDbDataContext _mongoDbDataContext;

        public ReturnOrderService(ILogger<ReturnOrderService> logger, AsyncRetryPolicy retryPolicy, IMongoDbDataContext mongoDbDataContext)
        {
            _logger = logger;
            _retryPolicy = retryPolicy;
            _mongoDbDataContext = mongoDbDataContext;
        }

        public async Task<BaseResponse<ReturnOrderModel>> GetLastStateByCorrelationId(Guid correlationId)
        {
            var filter = Builders<ReturnOrderModel>.Filter.Eq(fp => fp.CorrelationId, correlationId);

            var result = await _mongoDbDataContext.ReturnOrderModel.Find(filter).SortByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

            return new BaseResponse<ReturnOrderModel> { Data = result };
        }

        public async Task<BaseResponse<ReturnOrderModel>> GetLastStateByTraceCode(string traceCode)
        {
            var filter = Builders<ReturnOrderModel>.Filter.Eq(fp => fp.TraceCode, traceCode);

            var result = await _mongoDbDataContext.ReturnOrderModel.Find(filter).SortByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

            return new BaseResponse<ReturnOrderModel> { Data = result };
        }

        public async Task InsertReturnOrder(ReturnOrderModel model)
        {
            try
            {
                Task executionTask = _retryPolicy.ExecuteAsync(async () =>
                {
                    await _mongoDbDataContext.ReturnOrderModel.InsertOneAsync(model);
                });

                await executionTask;

            }
            catch (Exception ex)
            {
                _logger.LogWithCorrelationId(LogLevel.Error, model.CorrelationId, ex, $"Insert mongo error occurred - {ex.Message}");
            }
        }
    }
}
