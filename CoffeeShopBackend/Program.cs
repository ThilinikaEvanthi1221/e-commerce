using CoffeeShopBackend.Models;
using CoffeeShopBackend.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetValue<string>("MongoDBSettings:ConnectionString")));
builder.Services.AddSingleton<ProductService>(); 

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.MapGet("/products", async (ProductService productService) =>
    await productService.GetProductsAsync())
    .WithName("GetProducts");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class MongoDBSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}

public class Product
{
    public string Id { get; set; } = null!; 
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
}

public class ProductService
{
    private readonly IMongoCollection<Product> _products;

    public ProductService(IOptions<MongoDBSettings> mongoSettings)
    {
        var client = new MongoClient(mongoSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _products = database.GetCollection<Product>("Products");
    }

    public async Task<List<Product>> GetProductsAsync() =>
        await _products.Find(_ => true).ToListAsync();

    public async Task AddProductAsync(Product product) =>
        await _products.InsertOneAsync(product);
}
