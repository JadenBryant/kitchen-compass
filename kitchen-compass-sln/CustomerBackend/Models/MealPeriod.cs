using CustomerBackend.Models;

namespace CustomerBackend.Models
{
    public class MealPeriod
    {
        public string Name { get; set; }
        public Menu Menu { get; set; }
        public TimePeriod ActivePeriod { get; set; }

        public MealPeriod(string name, Menu menu)
        {
            Name = name;
            Menu = menu;
        }
    }
}
