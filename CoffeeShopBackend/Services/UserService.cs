using MongoDB.Driver;
using CoffeeShopBackend.Models;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("CoffeShopDB");
        _users = database.GetCollection<User>("Users");
    }

    public async Task<User> GetUserByUsernameAsync(string username) =>
        await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

    public async Task AddUserAsync(User user) =>
        await _users.InsertOneAsync(user);
}
