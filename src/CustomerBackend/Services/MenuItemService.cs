using CustomerBackend.Models;
using MongoDB.Driver;

namespace CustomerBackend.Services;

public class MenuItemService
{
    private readonly IMongoCollection<MenuItem> _items;

    public MenuItemService(IMongoDatabase db)
    {
        _items = db.GetCollection<MenuItem>("menuItems");
    }

    public async Task<MenuItem> CreateAsync(MenuItem item)
    {
        await _items.InsertOneAsync(item);
        return item;
    }
    
    public async Task<MenuItem?> GetByIdAsync(Guid id)
        => await _items.Find(item => item.Id == id).FirstOrDefaultAsync();

    public async Task<MenuItem?> UpdateAsync(Guid id, MenuItem item)
    {
        var result = await _items.ReplaceOneAsync(i => i.Id == id, item);
        return result.MatchedCount > 0 ? item : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _items.DeleteOneAsync(item => item.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<List<MenuItem>> GetAllAsync()
        => await _items.Find(_ => true).ToListAsync();
}