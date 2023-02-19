using MongoDB.Driver;
using ReturnOrderService.Models.Domain;

namespace ReturnOrderService.Data.Interfaces
{
    public interface IMongoDbDataContext
    {
        IMongoCollection<ReturnOrderModel> ReturnOrderModel { get; }
    }
}
