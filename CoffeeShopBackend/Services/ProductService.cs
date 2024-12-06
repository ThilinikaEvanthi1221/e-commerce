using CoffeeShopBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CoffeeShopBackend.Config;
using System.Collections.Generic;

namespace CoffeeShopBackend.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoSettings)
        {
            var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        // Get all products synchronously
        public List<Product> Get() =>
            _products.Find(_ => true).ToList();  // Using synchronous version of ToList()

        // Create a new product synchronously
        public void Create(Product product) =>
            _products.InsertOne(product);  // Using synchronous version of InsertOne
    }
}
