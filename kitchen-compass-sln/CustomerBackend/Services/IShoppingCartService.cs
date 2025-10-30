using CustomerBackend.Domain.Cart;
using CustomerBackend.Domain.Menu;
using System.Collections.Generic;

namespace CustomerBackend.Services
{
    public interface IShoppingCartService
    {
        void Add(IMenuItem item, int quantity);
        void Remove(IMenuItem item, int quantity);
        void Clear();
        float Subtotal();
        List<CartLine> Items();
    }

}