// File: Services/ProductService.cs
using CoffeeShopBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CoffeeShopBackend.Config;

namespace CoffeeShopBackend.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDBSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        // Get all products
        public async Task<List<Product>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        // Create a new product
        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);
    }
}
