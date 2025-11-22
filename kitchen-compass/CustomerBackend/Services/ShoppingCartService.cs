using CustomerBackend.Models;

namespace CustomerBackend.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ShoppingCart _cart = new();

        public void Add(IMenuItem item, int quantity) => _cart.AddItem(item, quantity);

        public void Remove(IMenuItem item, int quantity) => _cart.RemoveItem(item, quantity);

        public void Clear() => _cart.ClearCart();

        public float Subtotal() => _cart.Subtotal;

        public List<CartLine> Items() => _cart.GetItems();
    }
}