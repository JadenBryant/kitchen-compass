using CustomerBackend.Models;
using MongoDB.Driver;

namespace CustomerBackend.Services;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IMongoDatabase db)
    {
        _users = db.GetCollection<User>("users");
    }

    public async Task<User> CreateAsync(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<User?> GetByFirebaseIdAsync(string firebaseId)
        => await _users.Find(u => u.FirebaseId == firebaseId).FirstOrDefaultAsync();
}