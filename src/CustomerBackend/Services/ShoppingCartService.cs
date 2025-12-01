using CustomerBackend.Models;

namespace CustomerBackend.Services;

public class ShoppingCartService
{
    private readonly ShoppingCart _cart = new();
    
    public List<CartItem> Items => _cart.Items;

    public void Add(MenuItem item, int quantity) => _cart.AddItem(item, quantity);

    public void Remove(MenuItem item, int quantity) => _cart.RemoveItem(item, quantity);

    public void Clear() => _cart.ClearCart();

    public decimal Subtotal() => _cart.Subtotal;
}
