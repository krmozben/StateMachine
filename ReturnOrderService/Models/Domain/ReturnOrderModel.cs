using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ReturnOrderService.Configuration.Constants;

namespace ReturnOrderService.Models.Domain
{
    public class ReturnOrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string TraceCode { get; set; }
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public Guid CorrelationId { get; set; }
        [BsonRequired]
        public string State { get; set; }
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public ReturnType ReturnType { get; set; }
        [BsonRequired]
        public DateTime CreatedAt { get; set; }
        [BsonRequired]
        public string Model { get; set; }
    }
}
