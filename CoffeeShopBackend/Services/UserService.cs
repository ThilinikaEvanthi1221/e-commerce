using MongoDB.Driver;
using Microsoft.Extensions.Options;
using CoffeeShopBackend.Models;

public class UserService
{
    private readonly IMongoCollection<User> _users;
    
    // Inject MongoClient and MongoDBSettings using IOptions
    public UserService(IMongoClient mongoClient, IOptions<MongoDBSettings> mongoSettings)
    {
        var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
        _users = database.GetCollection<User>("Users");
    }

    // Get user by username
    public async Task<User> GetUserByUsernameAsync(string username) =>
        await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

    // Add a new user to the database
    public async Task AddUserAsync(User user) =>
        await _users.InsertOneAsync(user);
}
