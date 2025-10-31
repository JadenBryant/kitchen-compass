namespace CustomerBackend.Services
{
    public class MenuService
    {
        private readonly Menu _menu = new();

        public void Add(MenuItem item) => _menu.AddItem(item);

        public void Remove(Guid id) => _menu.RemoveItem(id);

        public List<MenuItem> Items() => _menu.Items;
    }
}
