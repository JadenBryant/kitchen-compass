using CustomerBackend.DTOs.Cart;
using CustomerBackend.Models;
using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    /// <summary>
    /// Field for the menu service.
    /// </summary>
    private readonly MenuService _service;
    
    /// <summary>
    /// Constructor for the menu controller.
    /// </summary>
    /// <param name="service">The menu service.</param>
    public MenuController(MenuService service) => _service = service;
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string menuId)
        => Ok(new { items = await _service.GetItemsAsync(menuId) });
    
    [HttpPost("add_item")]
    public async Task<IActionResult> AddItem([FromQuery] string menuId, [FromBody] CartItemDto dto)
    {
        var item = new MenuItem(dto.Name, dto.Price)
        {
            Id = dto.Id,
            Description = dto.Description ?? string.Empty,
            Ingredients = dto.Ingredients ?? string.Empty,
            CalorieCount = dto.CalorieCount ?? 0
        };
    
        await _service.AddItemAsync(menuId, item);
        return Ok(new { items = await _service.GetItemsAsync(menuId) });
    }
    
    [HttpDelete("remove_item")]
    public async Task<IActionResult> RemoveItem([FromQuery] string menuId, [FromQuery] Guid itemId)
    {
        await _service.RemoveItemAsync(menuId, itemId);
        return Ok(new { items = await _service.GetItemsAsync(menuId) });
    }
    
    [HttpPut("edit_item_price")]
    public async Task<IActionResult> EditItemPrice([FromQuery] string menuId, [FromQuery] Guid itemId, [FromQuery] decimal price)
    {
        await _service.EditItemPriceAsync(menuId, itemId, price);
        return Ok(new { items = await _service.GetItemsAsync(menuId) });
    }
}