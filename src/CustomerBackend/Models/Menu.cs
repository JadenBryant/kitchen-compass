namespace CustomerBackend.Models;
    
    public class Menu 
    {
        public Guid Id { get; set; }
        public string MenuName { get; set; }
        public List<Guid> Items { get; set; } = new List<Guid>();
    
        public Menu(string menuName) 
        {
            MenuName = menuName;
        }
    }