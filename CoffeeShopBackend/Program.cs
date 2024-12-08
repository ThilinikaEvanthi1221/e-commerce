using CoffeeShopBackend.Models;
using CoffeeShopBackend.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var builder = WebApplication.CreateBuilder(args);

// Register MongoDBSettings configuration from appsettings.json
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Register MongoClient as a singleton (because it's intended to be shared across the app)
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetValue<string>("MongoDBSettings:ConnectionString")));

// Register ProductService with transient lifetime, since it's stateless
builder.Services.AddTransient<ProductService>();

// Add MVC or API controllers
builder.Services.AddControllers();

// Swagger configuration for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// API routes are now mapped using controllers, no need for manual MapGet
app.MapControllers();

app.Run();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();


// MongoDBSettings class to bind configuration settings
public class MongoDBSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}

// Product model
public class Product
{
    [BsonId]  // Mark this property as the MongoDB _id
    [BsonRepresentation(BsonType.ObjectId)] 
    public ObjectId? Id { get; set; } = null!;
    [BsonElement("name")]
    public string Name { get; set; } = null!;
     [BsonElement("description")]
    public string Description { get; set; } = null!;
    [BsonElement("price")]
    public decimal Price { get; set; }
    [BsonElement("image")]
    public string ImageUrl { get; set; } = null!;
}

// ProductService class to interact with MongoDB
public class ProductService
{
    private readonly IMongoCollection<Product> _products;

    // Constructor receives MongoClient and MongoDBSettings via dependency injection
    public ProductService(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoSettings)
    {
        var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
        _products = database.GetCollection<Product>("Products");
    }

    public async Task<List<Product>> GetProductsAsync() =>
        await _products.Find(_ => true).ToListAsync();

    public async Task AddProductAsync(Product product) =>
        await _products.InsertOneAsync(product);
}


