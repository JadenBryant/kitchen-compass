namespace CustomerBackend.Models;

public interface IMenuItem
{
    Guid Id { get; }        // Unique identifier for each menu item
    string Name { get; }     // Name of the menu item
    decimal Price { get; }   // Price of the menu item
}