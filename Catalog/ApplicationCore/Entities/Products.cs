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
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("CategoryID")]
        public string CategoryID { get; set; }
        [BsonElement("Price")]
       // [BsonSerializer(typeof(StringOrInt32Serializer))]
        public int Price { get; set; }
        [BsonElement("Product_Image")]
        //[BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public string Product_Image { get; set; }
        [BsonElement("SKU")]
        public int SKU { get; set; }
        [BsonElement("InActive")]
        public bool InActive { get; set; }
    }
}
