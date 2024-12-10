using MongoDB.Bson; 
using MongoDB.Bson.Serialization.Attributes;

namespace CoffeeShopBackend.Models
{
    public class Product
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }  // Use ObjectId instead of int
       
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("price")]
        public decimal Price { get; set; }
        
        [BsonElement("image")]
        public string ImageUrl { get; set; }

         public Product()
    {
        if (Id == ObjectId.Empty)
        {
            Id = ObjectId.GenerateNewId();
        }
    }
    }
}
