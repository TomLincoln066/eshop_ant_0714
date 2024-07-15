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
    public class ProductCategory
    {
        [BsonId()]
        [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Parent_CategoryId")]
        public string Parent_CategoryId { get; set; }
     
    }
}
