using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaStore.Application.Services.DTO;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Services.Interfaces.Services
{
    public interface IProductService
    {
        Task<long?> CreateProductAsync(
            CreateProductDTO createProduct);

        Task UpdateProductAsync(
            long? productId,
            UpdateProductDTO updateProduct);

        Task DeleteProductAsync(
           long? productId);

        Task UpdateProductPriceAsync(
            long? productId,
            UpdateProductPriceDTO updateProductPrice);

        Task<ProductDetailsDTO> GetProductDetailsAsync(
            long? productId);

        Task<IEnumerable<ProductShortDTO>> GetProductListAsync(
            long? categoryId);
    }
}
