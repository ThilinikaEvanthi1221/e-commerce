using CoffeeShopBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CoffeeShopBackend.Config;
using System.Collections.Generic;
using MongoDB.Bson;

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

        // Create a new product
        public async Task CreateProductAsync(Product newProduct)
        {
            await _products.InsertOneAsync(newProduct);
        }

        // Get all products
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        // Get product by ID
        public async Task<Product> GetProductByIdAsync(ObjectId id)
        {
            var product = await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }

        // Update a product by ID
        public async Task UpdateProductAsync(ObjectId id, Product updatedProduct)
        {
            await _products.ReplaceOneAsync(p => p.Id == id, updatedProduct);
        }

        // Delete a product by ID
        public async Task DeleteProductAsync(ObjectId id)
        {
            await _products.DeleteOneAsync(p => p.Id == id);
        }
    }
}
