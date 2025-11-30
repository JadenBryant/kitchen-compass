using CustomerBackend.Models;
using MongoDB.Driver;

namespace CustomerBackend.Services;

public class MenuItemService
{
    private readonly IMongoCollection<MenuItem> _items;

    public MenuItemService(IMongoDatabase db)
    {
        _items = db.GetCollection<MenuItem>("MenuItems");
    }

    public async Task<MenuItem> CreateAsync(MenuItem item)
    {
        await _items.InsertOneAsync(item);
        return item;
    }

    public async Task<List<MenuItem>> GetAllAsync()
        => await _items.Find(_ => true).ToListAsync();
}