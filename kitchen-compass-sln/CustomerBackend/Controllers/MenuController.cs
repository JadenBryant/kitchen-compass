using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase {
    [HttpGet]
    public IActionResult GetMenu() {
        var items = new[] {
            new { Id = 1, Name = "Latte", Price = 4.50 },
            new { Id = 2, Name = "Croissant", Price = 3.25 }
        };

        return Ok(items);
    }
}