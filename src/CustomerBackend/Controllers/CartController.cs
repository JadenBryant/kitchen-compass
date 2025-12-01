using System.Text.Json;
using CustomerBackend.DTOs.Cart;
using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly CartService _cart;
    private readonly MenuItemService _menuItemService;
    private readonly UserService _userService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _firebaseApiKey;
    private readonly JsonSerializerOptions _jsonOptions;

    public CartController(
        CartService cart,
        MenuItemService menuItemService,
        UserService userService,
        IHttpClientFactory httpClientFactory,
        IConfiguration config)
    {
        _cart = cart;
        _menuItemService = menuItemService;
        _userService = userService;
        _httpClientFactory = httpClientFactory;
        _firebaseApiKey = config["FIREBASE_API_KEY"] ?? throw new Exception("Missing FIREBASE_API_KEY");
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = await GetUserIdAsync();
        if (userId == null)
            return Unauthorized("Invalid or missing authentication token.");

        var cart = await _cart.GetOrCreateCartAsync(userId);
        return Ok(new { items = cart.Items, subtotal = cart.Subtotal });
    }

    [HttpPost("add_item")]
    public async Task<IActionResult> Add([FromBody] CartItemDto dto)
    {
        if (dto.Qty <= 0)
            return BadRequest("Quantity must be greater than 0.");

        var userId = await GetUserIdAsync();
        if (userId == null)
            return Unauthorized("Invalid or missing authentication token.");

        var cart = await _cart.GetOrCreateCartAsync(userId);
    
        var menuItem = await _menuItemService.GetByIdAsync(dto.MenuItemId);
        if (menuItem == null)
            return NotFound($"Menu item with ID {dto.MenuItemId} not found.");

        cart.AddItem(menuItem, dto.Qty);
        await _cart.SaveCartAsync(cart);
    
        return Ok(new { items = cart.Items, subtotal = cart.Subtotal });
    }

    [HttpPost("remove_item")]
    public async Task<IActionResult> Remove([FromBody] CartItemDto dto)
    {
        if (dto.Qty <= 0)
            return BadRequest("Quantity must be greater than 0.");

        var userId = await GetUserIdAsync();
        if (userId == null)
            return Unauthorized("Invalid or missing authentication token.");

        var cart = await _cart.GetOrCreateCartAsync(userId);
    
        var menuItem = await _menuItemService.GetByIdAsync(dto.MenuItemId);
        if (menuItem == null)
            return NotFound($"Menu item with ID {dto.MenuItemId} not found.");

        cart.RemoveItem(menuItem, dto.Qty);
        await _cart.SaveCartAsync(cart);
    
        return Ok(new { items = cart.Items, subtotal = cart.Subtotal });
    }

    [HttpDelete("clear_items")]
    public async Task<IActionResult> Clear()
    {
        var userId = await GetUserIdAsync();
        if (userId == null)
            return Unauthorized("Invalid or missing authentication token.");

        var cart = await _cart.GetOrCreateCartAsync(userId);
        cart.ClearCart();
        await _cart.SaveCartAsync(cart);
        return Ok();
    }
    
    private async Task<string?> GetUserIdAsync()
    {
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return null;

        var token = authHeader.Substring("Bearer ".Length).Trim();

        var httpClient = _httpClientFactory.CreateClient();
        var payload = new { idToken = token };
        var response = await httpClient.PostAsJsonAsync(
            $"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={_firebaseApiKey}",
            payload,
            _jsonOptions
        );

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(content);

        if (json.TryGetProperty("users", out var users) && users.GetArrayLength() > 0)
        {
            var user = users[0];
            if (user.TryGetProperty("localId", out var localId))
            {
                var firebaseId = localId.GetString();
                var dbUser = await _userService.GetByFirebaseIdAsync(firebaseId);
                return dbUser?.Id;
            }
        }

        return null;
    }

}