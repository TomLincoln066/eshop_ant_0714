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
    public class VariationValue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        [BsonElement("VariationId")]
        public string VariationId { get; set; }
        [BsonElement("Value")]
        public string Value { get; set; }
    }
}
