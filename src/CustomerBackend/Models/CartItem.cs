namespace CustomerBackend.Models;

public class CartItem
{
    public Guid MenuItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal {
        get => Price * Quantity;
    }
}