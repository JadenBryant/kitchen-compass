using CustomerBackend.Models;

namespace CustomerBackend.Services
{
    public interface IShoppingCartService
    {
        void Add(IMenuItem item, int quantity);
        void Remove(IMenuItem item, int quantity);
        void Clear();
        decimal Subtotal();
        List<CartLine> Items();
    }

}