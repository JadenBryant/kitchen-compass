using CustomerBackend.DTOs.Cart;
using CustomerBackend.Models;
using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ShoppingCartService _cart;
    public CartController(ShoppingCartService cart) => _cart = cart;

    [HttpGet]
    public IActionResult Get()
        => Ok(new { items = _cart.Items, subtotal = _cart.Subtotal() });

    [HttpPost("add_item")]
    public IActionResult Add([FromBody] CartItemDto dto)
    {
        var item = new MenuItem(dto.Name, dto.Price)
        {
            Id = dto.Id,
            Description = dto.Description ?? string.Empty,
            Ingredients = dto.Ingredients ?? string.Empty,
            CalorieCount = dto.CalorieCount ?? 0
        };

        _cart.Add(item, dto.Qty);
        return Ok(new { items = _cart.Items, subtotal = _cart.Subtotal() });
    }

    [HttpPost("remove_item")]
    public IActionResult Remove([FromBody] CartItemDto dto)
    {
        var item = new MenuItem(dto.Name, dto.Price)
        {
            Id = dto.Id,
            Description = dto.Description ?? string.Empty,
            Ingredients = dto.Ingredients ?? string.Empty,
            CalorieCount = dto.CalorieCount ?? 0
        };

        _cart.Remove(item, dto.Qty);
        return Ok(new { items = _cart.Items, subtotal = _cart.Subtotal() });
    }

    [HttpDelete("clear_items")]
    public IActionResult Clear()
    {
        _cart.Clear();
        return Ok();
    }
}
