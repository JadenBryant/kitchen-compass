using CustomerBackend.Models;

namespace CustomerBackend.Services
{
    public interface IShoppingCartService
    {
        void Add(MenuItem item, int quantity);
        void Remove(MenuItem item, int quantity);
        void Clear();
        decimal Subtotal();
        List<CartItem> Items();
    }

}