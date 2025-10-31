namespace CustomerBackend.Services
{
    public class MenuService
    {
        private readonly Menu _menu = new();

        public void Add(MenuItem item) => _menu.AddItem(item);

        public void Remove(Guid id) => _menu.RemoveMenuItem(id);
        public void EditItemPrice(Guid itemId, float newPrice) => _menu.EditMenuItemPrice(itemId, newPrice);

        public List<MenuItem> Items() => _menu.Items;
    }
}
