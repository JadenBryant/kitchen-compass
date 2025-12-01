using CustomerBackend.Models;
using MongoDB.Driver;

namespace CustomerBackend.Services;

public class CartService
{
    private readonly IMongoCollection<ShoppingCart> _carts;

    public CartService(IMongoDatabase db)
    {
        _carts = db.GetCollection<ShoppingCart>("shoppingCarts");
    }

    public async Task<ShoppingCart> GetOrCreateCartAsync(string userId)
    {
        var cart = await _carts.Find(c => c.UserId == userId).FirstOrDefaultAsync();

        if (cart == null)
        {
            cart = new ShoppingCart { UserId = userId };
            await _carts.InsertOneAsync(cart);
        }

        return cart;
    }

    public async Task SaveCartAsync(ShoppingCart cart)
    {
        cart.LastUpdated = DateTime.UtcNow;
        await _carts.ReplaceOneAsync(c => c.UserId == cart.UserId, cart);
    }

    public async Task<bool> DeleteCartAsync(string userId)
    {
        var result = await _carts.DeleteOneAsync(c => c.UserId == userId);
        return result.DeletedCount > 0;
    }
}