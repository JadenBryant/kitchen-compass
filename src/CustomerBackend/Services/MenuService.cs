using CustomerBackend.Models;
using MongoDB.Driver;

namespace CustomerBackend.Services;

public class MenuService {
    private readonly IMongoCollection<Menu> _menuCollection;

    public MenuService(IMongoDatabase db) {
        _menuCollection = db.GetCollection<Menu>("menus");
    }

    public async Task<Menu> CreateMenuAsync(string menuName) {
        var menu = new Menu(menuName) {
            Id = Guid.NewGuid()
        };
        await _menuCollection.InsertOneAsync(menu);
        return menu;
    }

    public async Task<Menu?> GetMenuAsync(string menuId) {
        if (!Guid.TryParse(menuId, out var guid))
            return null;

        return await _menuCollection.Find(m => m.Id == guid).FirstOrDefaultAsync();
    }

    public async Task AddItemAsync(string menuId, Guid itemId) {
        if (!Guid.TryParse(menuId, out var guid))
            return;

        var update = Builders<Menu>.Update.AddToSet(m => m.Items, itemId);
        await _menuCollection.UpdateOneAsync(m => m.Id == guid, update);
    }

    public async Task RemoveItemAsync(string menuId, Guid itemId) {
        if (!Guid.TryParse(menuId, out var guid))
            return;

        var update = Builders<Menu>.Update.Pull(m => m.Items, itemId);
        await _menuCollection.UpdateOneAsync(m => m.Id == guid, update);
    }

    public async Task<bool> UpdateMenuNameAsync(string menuId, string newName) {
        if (!Guid.TryParse(menuId, out var guid))
            return false;

        var update = Builders<Menu>.Update.Set(m => m.MenuName, newName);
        var result = await _menuCollection.UpdateOneAsync(m => m.Id == guid, update);
        return result.ModifiedCount > 0;
    }

    public async Task<List<Guid>> GetItemsAsync(string menuId) {
        var menu = await GetMenuAsync(menuId);
        return menu?.Items ?? new List<Guid>();
    }

    public async Task<List<Menu>> GetAllMenusAsync() {
        return await _menuCollection.Find(_ => true).ToListAsync();
    }
}