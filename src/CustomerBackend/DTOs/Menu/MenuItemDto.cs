namespace CustomerBackend.DTOs.Menu;

// DTO the API receives
public class MenuItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }

    // Optional; present to satisfy non-nullable properties on MenuItem
    public string? Description { get; set; }
    public string? Ingredients { get; set; }
    public int? CalorieCount { get; set; }
    public string? ImageName { get; set; }
}
