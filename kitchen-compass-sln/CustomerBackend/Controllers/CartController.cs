using Microsoft.AspNetCore.Mvc;
using CustomerBackend.Services;
using CustomerBackend;

namespace CustomerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartService _cart;
        public CartController(IShoppingCartService cart) => _cart = cart;

        // DTO the API receives
        public class ItemDto
        {
            public Guid Id { get; set; }                 // <-- Guid (matches IMenuItem)
            public string Name { get; set; } = "";
            public float Price { get; set; }
            public int Qty { get; set; }

            // Optional; present to satisfy non-nullable properties on MenuItem
            public string? Description { get; set; }
            public string? Ingredients { get; set; }
            public int? CalorieCount { get; set; }
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(new { items = _cart.Items(), subtotal = _cart.Subtotal() });

        [HttpPost("add")]
        public IActionResult Add([FromBody] ItemDto dto)
        {
            var item = new MenuItem(dto.Name, dto.Price)
            {
                Id = dto.Id,
                Description = dto.Description ?? string.Empty,
                Ingredients = dto.Ingredients ?? string.Empty,
                CalorieCount = dto.CalorieCount ?? 0
            };

            _cart.Add(item, dto.Qty);
            return Ok(new { items = _cart.Items(), subtotal = _cart.Subtotal() });
        }

        [HttpPost("remove")]
        public IActionResult Remove([FromBody] ItemDto dto)
        {
            var item = new MenuItem(dto.Name, dto.Price)
            {
                Id = dto.Id,
                Description = dto.Description ?? string.Empty,
                Ingredients = dto.Ingredients ?? string.Empty,
                CalorieCount = dto.CalorieCount ?? 0
            };

            _cart.Remove(item, dto.Qty);
            return Ok(new { items = _cart.Items(), subtotal = _cart.Subtotal() });
        }

        [HttpDelete("clear")]
        public IActionResult Clear()
        {
            _cart.Clear();
            return Ok();
        }
    }
}

