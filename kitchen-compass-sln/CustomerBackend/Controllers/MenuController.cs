using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly MenuService _menu;
    public MenuController(MenuService menu) => _menu = menu;
    
    [HttpGet]
    public IActionResult Get()
            => Ok(new { items = _menu.Items() });

    [HttpPost("add_item")]
    public IActionResult AddItem([FromQuery] ItemDto dto)
    {
        var item = new MenuItem(dto.Name, dto.Price)
        {
            Id = dto.Id,
            Description = dto.Description ?? string.Empty,
            Ingredients = dto.Ingredients ?? string.Empty,
            CalorieCount = dto.CalorieCount ?? 0
        };

        _menu.Add(item);
        return Ok(new { items = _menu.Items() });
    }

    [HttpDelete("remove_item")]
    public IActionResult RemoveItem([FromQuery] string id)
    {
        _menu.Remove(Guid.Parse(id));
        return Ok(new { items = _menu.Items() });
    }
}