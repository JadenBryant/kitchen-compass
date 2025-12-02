namespace CustomerBackend.DTOs.Cart;

public class CartItemDto
{
    public Guid MenuItemId { get; set; }
    public int Qty { get; set; } = 1;
}
