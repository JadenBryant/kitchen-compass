using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerBackend.Models;

/// <summary>
/// Represents a shopping cart of items for an order.
/// </summary>
public class ShoppingCart
{
    /// <summary>
    /// Unique identifier for the shopping cart.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    /// <summary>
    /// Identifier for the user who owns the cart.
    /// </summary>
    [BsonElement("userId")]
    public string UserId { get; set; } = "";
    
    /// <summary>
    /// List of all items currently in the cart.
    /// </summary>
    [BsonElement("items")]
    public List<CartItem> Items { get; set; } = new();
    
    /// <summary>
    /// Last time the cart was updated.
    /// </summary>
    [BsonElement("lastUpdated")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
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
        var existingItem = Items.FirstOrDefault(cartItem => cartItem.MenuItemId == item.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            Items.Add(new CartItem(item.Id)
            {
                Name = item.Name,
                Price = item.Price,
                Quantity = quantity
            });
        }

        LastUpdated = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Removes an item from the shopping cart.
    /// </summary>
    /// <param name="item">A menu item.</param>
    /// <param name="quantity">The number of items to remove.</param>
    public void RemoveItem(MenuItem item, int quantity = 1)
    {
        var existingItem = Items.FirstOrDefault(cartItem => cartItem.MenuItemId == item.Id);

        if (existingItem != null)
        {
            existingItem.Quantity -= quantity;

            if (existingItem.Quantity <= 0)
            {
                Items.Remove(existingItem);
            }
        }

        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes all items from the cart.
    /// </summary>
    public void ClearCart()
    {
        Items.Clear();
        LastUpdated = DateTime.UtcNow;
    }
}
