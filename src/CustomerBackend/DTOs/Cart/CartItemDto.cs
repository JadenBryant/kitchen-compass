namespace CustomerBackend.DTOs.Cart;

public class CartItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Qty { get; set; }

    // Optional; present to satisfy non-nullable properties on MenuItem
    public string? Description { get; set; }
    public string? Ingredients { get; set; }
    public int? CalorieCount { get; set; }
    public string ImageName { get; set; }
}
