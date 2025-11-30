using CustomerBackend.Models;
using MongoDB.Driver;

namespace CustomerBackend.Services;

public class MenuService
{
    private readonly IMongoCollection<Menu> _menuCollection;

    public MenuService(IMongoDatabase db)
    {
        _menuCollection = db.GetCollection<Menu>("Menus");
    }

    public async Task<Menu?> GetMenuAsync(string menuId)
    {
        return await _menuCollection.Find(m => m.Id == menuId).FirstOrDefaultAsync();
    }

    public async Task AddItemAsync(string menuId, MenuItem item)
    {
        var update = Builders<Menu>.Update.Push(m => m.Items, item);
        await _menuCollection.UpdateOneAsync(m => m.Id == menuId, update);
    }

    public async Task RemoveItemAsync(string menuId, Guid itemId)
    {
        var update = Builders<Menu>.Update.PullFilter(m => m.Items, i => i.Id == itemId);
        await _menuCollection.UpdateOneAsync(m => m.Id == menuId, update);
    }

    public async Task EditItemPriceAsync(string menuId, Guid itemId, decimal newPrice)
    {
        var filter = Builders<Menu>.Filter.And(
            Builders<Menu>.Filter.Eq(m => m.Id, menuId),
            Builders<Menu>.Filter.ElemMatch(m => m.Items, i => i.Id == itemId)
        );
        var update = Builders<Menu>.Update.Set("Items.$.Price", newPrice);
        await _menuCollection.UpdateOneAsync(filter, update);
    }

    public async Task<List<MenuItem>> GetItemsAsync(string menuId)
    {
        var menu = await GetMenuAsync(menuId);
        return menu?.Items ?? new List<MenuItem>();
    }
}