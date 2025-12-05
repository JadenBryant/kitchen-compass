namespace CustomerBackend.Models;

public class MenuItem : IMenuItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public int? CalorieCount { get; set; }
    public string ImageName { get; set; }
    
    //This dictionary serves as a list of potential modifications when used in a menu context, and as a list of to-order modifications when used in a shopping-cart context.
    // TODO: There should be a separate class for CartItem.
    // public Dictionary<string, bool> Modifications { get; set; } = new Dictionary<string, bool>(); //Modifications are in the format <modification (no meat, no sauce, etc), modification applied? (T/F)>

    public MenuItem(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    // TODO: move to CartItem class
    // public void AddModification(string mod)
    // {
    //     Modifications.Add(mod, false);
    // }
    
    public void AddToCart()
    {
        //TODO: implement in separate CartItem class
    }

    public void AddToMenu()
    {
        //TODO: implement in separate Menu class
    }
}
