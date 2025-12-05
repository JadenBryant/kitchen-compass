using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly ConfigService _service;

    public ConfigController(ConfigService service) => _service = service;

    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var value = await _service.GetValueAsync(key);
        if (value == null)
            return NotFound();
        
        return Ok(new { key, value });
    }

    [HttpPost("set")]
    public async Task<IActionResult> Set([FromQuery] string key, [FromBody] string value)
    {
        await _service.SetValueAsync(key, value);
        return Ok(new { key, value });
    }
}