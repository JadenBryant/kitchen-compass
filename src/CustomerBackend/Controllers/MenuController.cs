using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase {
    private readonly MenuService _service;

    public MenuController(MenuService service) => _service = service;

    [HttpPost("create_menu")]
    public async Task<IActionResult> CreateMenu([FromBody] string menuName) {
        var menu = await _service.CreateMenuAsync(menuName);
        return Ok(new {
            id = menu.Id, name = menu.MenuName
        });
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string menuId) {
        var menu = await _service.GetMenuAsync(menuId);
        if (menu == null)
            return NotFound();

        return Ok(new {
            menuName = menu.MenuName, items = menu.Items
        });
    }
    
    [HttpGet("get_all")]
    public async Task<IActionResult> GetAll()
    {
        var menus = await _service.GetAllMenusAsync();
        return Ok(menus.Select(m => new {
            id = m.Id,
            name = m.MenuName,
            items = m.Items
        }));
    }

    [HttpPost("add_item")]
    public async Task<IActionResult> AddItem([FromQuery] string menuId, [FromQuery] Guid itemId) {
        await _service.AddItemAsync(menuId, itemId);
        var menu = await _service.GetMenuAsync(menuId);
        if (menu == null)
            return NotFound();

        return Ok(new {
            menuName = menu.MenuName, items = menu.Items
        });
    }

    [HttpDelete("remove_item")]
    public async Task<IActionResult> RemoveItem([FromQuery] string menuId, [FromQuery] Guid itemId) {
        await _service.RemoveItemAsync(menuId, itemId);
        var menu = await _service.GetMenuAsync(menuId);
        if (menu == null)
            return NotFound();

        return Ok(new {
            menuName = menu.MenuName, items = menu.Items
        });
    }

    [HttpPut("edit_menu_name")]
    public async Task<IActionResult> EditMenuName([FromQuery] string menuId, [FromBody] string newName) {
        var success = await _service.UpdateMenuNameAsync(menuId, newName);
        if (!success)
            return NotFound();

        var menu = await _service.GetMenuAsync(menuId);
        return Ok(new {
            menuName = menu.MenuName, items = menu.Items
        });
    }
}