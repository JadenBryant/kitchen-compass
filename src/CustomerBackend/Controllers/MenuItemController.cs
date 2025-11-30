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

    // Add Item
    [HttpPost("create_item")]
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

    // Get All Items
    [HttpGet("get_all")]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    // Get Item By ID
    [HttpGet("get_item/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    // Update Item
    [HttpPut("update_item/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MenuItemDto dto)
    {
        var item = new MenuItem(dto.Name, dto.Price)
        {
            Id = id,
            Description = dto.Description ?? string.Empty,
            Ingredients = dto.Ingredients ?? string.Empty,
            CalorieCount = dto.CalorieCount,
            ImageName = dto.ImageName ?? string.Empty,
        };

        var updated = await _service.UpdateAsync(id, item);
        if (updated == null)
            return NotFound();
        return Ok(updated);
    }

    // Remove Item
    [HttpDelete("remove_item/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}