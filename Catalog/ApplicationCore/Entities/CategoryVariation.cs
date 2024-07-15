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
    public class CategoryVariation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        [BsonElement("CategoryId")]
        public string CategoryId { get; set; }
        [BsonElement("VariationName")]
        public string VariationName { get; set; }
    }
}
