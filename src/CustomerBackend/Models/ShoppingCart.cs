namespace CustomerBackend.Models;

/// <summary>
/// Represents a shopping cart of items for an order.
/// </summary>
public class ShoppingCart
{
    /// <summary>
    /// Unique identifier for the shopping cart.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();
    
    /// <summary>
    /// List of all items currently in the cart.
    /// </summary>
    public readonly List<CartItem> Items = new List<CartItem>();
    
    /// <summary>
    /// Calculates the total cost of all items in the cart.
    /// </summary>
    public decimal Subtotal => Items.Sum(line => line.Subtotal);
    
    /// <summary>
    /// Adds an item to the shopping cart.
    /// </summary>
    /// <param name="item">A menu item.</param>
    /// <param name="quantity">The number of items to add.</param>
    public void AddItem(MenuItem item, int quantity = 1)
    {
        // Check if the item is already in the cart by comparing the item's ID.
        var existingItem = Items.FirstOrDefault(cartItem => cartItem.MenuItemId == item.Id);

        // If the item exists, just update the quantity
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            // Otherwise, add a new CartLine with the item and quantity
            Items.Add(new CartItem(item.Id));
        }
    }
    
    /// <summary>
    /// Removes an item from the shopping cart.
    /// </summary>
    /// <param name="item">A menu item.</param>
    /// <param name="quantity">The number of items to remove.</param>
    public void RemoveItem(MenuItem item, int quantity)
    {
        // Find the item to remove by comparing its ID.
        var existingItem = Items.FirstOrDefault(cartItem => cartItem.MenuItemId == item.Id);

        // If the item is found, reduce its quantity
        if (existingItem != null)
        {
            existingItem.Quantity -= quantity;

            // If quantity falls to zero or less, remove it completely
            if (existingItem.Quantity <= 0)
            {
                Items.Remove(existingItem);
            }
        }
    }

    /// <summary>
    /// Removes all items from the cart.
    /// </summary>
    public void ClearCart()
    {
        // Simply Clears all CartLine objects from the cart
        Items.Clear();
    }
}
