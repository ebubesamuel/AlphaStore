using AlphaStore.Application.Services.DTO;
using AlphaStore.Application.Services.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlphaStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(
            CreateUpdateCartDTO createUpdateCartDTO)
        {
            var shoppingCartId = await _shoppingCartService.CreateShoppingCart(
                createUpdateCartDTO.ProductId,
                createUpdateCartDTO.ProductCount);

            return Ok(shoppingCartId);
        }

        [HttpPost("add/item")]
        public async Task<IActionResult> AddItemAsync(
           CreateUpdateCartDTO createUpdateCartDTO)
        {
            var shoppingCartId = await _shoppingCartService.CreateShoppingCart(
               createUpdateCartDTO.ProductId,
               createUpdateCartDTO.ProductCount);

            return Ok(shoppingCartId);
        }

        [HttpGet("detail/{shoppingCartId}")]
        public async Task<IActionResult> GetDetailsAsync(
            long? shoppingCartId)
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCartDetailsAsync(
                shoppingCartId);

            return Ok(shoppingCart);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync()
        {
            var shoppingCarts = await _shoppingCartService.GetShoppingCartListAsync();

            return Ok(shoppingCarts);
        }
    }
}
