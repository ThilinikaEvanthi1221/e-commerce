using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("username")]
    [BsonRequired]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = null!;

    [BsonElement("email")]
    [BsonRequired]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; } = null!;

    // Not stored in DB, used just for registration
    [BsonIgnore]
    public string Password { get; set; } = null!;  // Used for handling plain text passwords during registration

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Optional: Indexes for unique constraints (in the database) for email and username
    // Ensure uniqueness is enforced during registration by validating before insertion
    [BsonIgnore]
    public bool IsEmailUnique { get; set; }

    [BsonIgnore]
    public bool IsUsernameUnique { get; set; }
}
