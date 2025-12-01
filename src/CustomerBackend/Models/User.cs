using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerBackend.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("firebaseId")]
    public string FirebaseId { get; set; } = "";

    [BsonElement("email")]
    public string Email { get; set; } = "";

    [BsonElement("firstName")]
    public string FirstName { get; set; } = "";

    [BsonElement("lastName")]
    public string LastName { get; set; } = "";

    [BsonElement("joinDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
}