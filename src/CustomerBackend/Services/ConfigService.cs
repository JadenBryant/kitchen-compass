using CustomerBackend.Models;
using MongoDB.Driver;

namespace CustomerBackend.Services;

public class ConfigService
{
    private readonly IMongoCollection<Config> _configCollection;

    public ConfigService(IMongoDatabase db)
    {
        _configCollection = db.GetCollection<Config>("config");
    }

    public async Task<string?> GetValueAsync(string key)
    {
        var config = await _configCollection
            .Find(c => c.Key == key)
            .FirstOrDefaultAsync();
        return config?.Value;
    }

    public async Task SetValueAsync(string key, string value)
    {
        var filter = Builders<Config>.Filter.Eq(c => c.Key, key);
        var update = Builders<Config>.Update.Set(c => c.Value, value);
        
        await _configCollection.UpdateOneAsync(
            filter,
            update,
            new UpdateOptions { IsUpsert = true }
        );
    }
}