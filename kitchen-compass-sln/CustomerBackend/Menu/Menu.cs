using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend;

public class Menu
{
    // add id for menu objects

    public List<MenuItem> Items = new List<MenuItem>();

    public string MenuType { get; set; }
    public string Id { get; set; }
    public Menu(string menutype, string id)
    {
        MenuType = menutype;
        Id = id;
    }

    public void AddItem(MenuItem newitem)
    {
        Items.Add(newitem);
    }

    public void DisplayMenu()
    {
        if (Items.Count != 0)
        {
            foreach (MenuItem item in Items)
            {
                Console.WriteLine(item);
            }
        }
        else
        {
            Console.WriteLine("no menu items");
        }
    }
}
