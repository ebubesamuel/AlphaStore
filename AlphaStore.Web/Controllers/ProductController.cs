using AlphaStore.Application.Services.DTO;
using AlphaStore.Application.Services.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AlphaStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAsync(
            CreateProductDTO createProduct)
        {
            var productId = await _productService.CreateProductAsync(createProduct);
            return Ok(productId);
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProductAsync(
            long? productId)
        {
            await _productService.DeleteProductAsync(productId);
            return Ok(productId);
        }

        [HttpPatch("update/{productId}")]
        public async Task<IActionResult> UpdateProductAsync(
            long? productId,
            UpdateProductDTO updateProduct)
        {
            await _productService.UpdateProductAsync(
                productId,
                updateProduct);

            return Ok(productId);
        }

        [HttpPost("update/{productId}/price")]
        public async Task<IActionResult> UpdateProductPriceAsync(
           long? productId,
           UpdateProductPriceDTO updateProductPrice)
        {
            await _productService.UpdateProductPriceAsync(
                productId,
                updateProductPrice);

            return Ok(productId);
        }

        [HttpGet("detail/{productId}")]
        public async Task<IActionResult> GetProductDetailsAsync(
            long? productId)
        {
            var product = await _productService.GetProductDetailsAsync(
               productId);

            return Ok(product);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetProductListAsync(
            [FromQuery] long? categoryId)
        {
            var products = await _productService.GetProductListAsync(
              categoryId);

            return Ok(products);
        }
    }
}
