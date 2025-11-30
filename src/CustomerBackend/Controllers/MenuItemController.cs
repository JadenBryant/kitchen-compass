using CustomerBackend.Models;
using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

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

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly MenuItemService _service;

    public MenuItemController(MenuItemService service)
        => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MenuItemDto dto)
    {
        var item = new MenuItem(dto.Name, dto.Price)
        {
            Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
            Description = dto.Description ?? string.Empty,
            Ingredients = dto.Ingredients ?? string.Empty,
            CalorieCount = dto.CalorieCount,
            ImageName = dto.ImageName ?? string.Empty,
        };

        var created = await _service.CreateAsync(item);
        return Ok(created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());
}