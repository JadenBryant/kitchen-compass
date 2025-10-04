using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend;

public class MenuItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public int CalorieCount { get; set; }

    //This dictionary serves as a list of potential modifications when used in a menu context, and as a list of to-order modifications when used in a shopping-cart context.
    public Dictionary<string, bool> Modifications { get; set; } //Modifications are in the format <modification (no meat, no sauce, etc), modification applied? (T/F)>

    public MenuItem(string id)
    {
        Id = id;
        Modifications = new Dictionary<string, bool>();
    }

    public void AddModification(string mod)
    {
        Modifications.Add(mod, false);
    }

    public void AddToCart()
    {
        //TODO: implement
    }

    public void AddToMenu()
    {
        //TODO: implement
    }
}
