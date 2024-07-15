using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [BsonIgnoreExtraElements]
    public class ProductVariationValues
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        [BsonElement("ProductId")]
        public string ProductId { get; set; }
        [BsonElement("VariationValueId")]
        public string VariationValueId { get; set; }
    }
}
