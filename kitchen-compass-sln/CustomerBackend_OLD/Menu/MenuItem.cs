using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend;

public interface IMenuItem
{
    Guid Id { get; }        // Unique identifier for each menu item
    string Name { get; }     // Name of the menu item
    float Price { get; }   // Price of the menu item
}

public class MenuItem : IMenuItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public int CalorieCount { get; set; }

    //This dictionary serves as a list of potential modifications when used in a menu context, and as a list of to-order modifications when used in a shopping-cart context.
    public Dictionary<string, bool> Modifications { get; set; } = new Dictionary<string, bool>(); //Modifications are in the format <modification (no meat, no sauce, etc), modification applied? (T/F)>

    public MenuItem(string name, float price)
    {
        Name = name;
        Price = price;
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
