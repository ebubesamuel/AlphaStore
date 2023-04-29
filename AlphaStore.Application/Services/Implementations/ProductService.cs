
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaStore.Application.Services.Interfaces;
using AlphaStore.Application.Services.Interfaces.Services;
using AlphaStore.Application.Models;
using AlphaStore.Application.Services.DTO;

namespace AlphaStore.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(
            IMapper mapper,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<long?> CreateProductAsync(
            CreateProductDTO createProduct)
        {
            var price = new Price(
                createProduct.PriceAmount,
                createProduct.PriceCurrency);

            var product = Product.Create(
                createProduct.Name,
                createProduct.Description,
                createProduct.CategoryId,
                createProduct.Quanitity,
                price);

            var isUnique = await _productRepository.IsProductUniqueAsync(
                product.Name);

            if (!isUnique)
            {
                throw new InvalidOperationException(
                    "Product name is not unique");
            }

            var productId = await _productRepository.SaveAsync(
                product);

            return productId;
        }

        public async Task DeleteProductAsync(
            long? productId)
        {
            if (productId is null)
            {
                throw new ArgumentNullException(
                    nameof(productId),
                    "Product id is requred");
            }

            var product = await _productRepository.GetAsync(
                productId);

            if (product is null)
            {
                throw new ArgumentException(
                    $"Product wit id {productId} does not exist",
                    nameof(product));
            }

            await _productRepository.DeleteAsync(product);
        }

        public async Task UpdateProductAsync(
            long? productId,
            UpdateProductDTO updateProduct)
        {
            if (productId is null)
            {
                throw new ArgumentNullException(
                    nameof(productId),
                    "Product id is requred");
            }

            var product = await _productRepository.GetAsync(
                productId);

            if (product is null)
            {
                throw new ArgumentException(
                    $"Product wit id {productId} does not exist",
                    nameof(product));
            }

            var isUnique = await _productRepository.IsProductUniqueAsync(
               updateProduct.Name,
               productId);

            if (!isUnique)
            {
                throw new InvalidOperationException(
                    "Product name is not unique");
            }

            product.Update(
                updateProduct.Name,
                updateProduct.Description,
                updateProduct.Quanitity);

            await _productRepository.UpdateAsync(product);
        }

        public async Task UpdateProductPriceAsync(
           long? productId,
           UpdateProductPriceDTO updateProductPrice)
        {
            if (productId is null)
            {
                throw new ArgumentNullException(
                    nameof(productId),
                    "Product id is requred");
            }

            var product = await _productRepository.GetAsync(
                productId);

            if (product is null)
            {
                throw new ArgumentException(
                    $"Product wit id {productId} does not exist",
                    nameof(product));
            }

            var newPrice = new Price(
                updateProductPrice.PriceAmount,
                updateProductPrice.PriceCurrency);

            product.ChangePrice(newPrice);

            await _productRepository.UpdateAsync(product);
        }

        public async Task<ProductDetailsDTO> GetProductDetailsAsync(
            long? productId)
        {
            if (productId is null)
            {
                throw new ArgumentNullException(
                    nameof(productId),
                    "Product id is requred");
            }

            var product = await _productRepository.GetAsync(
                productId);

            if (product is null)
            {
                throw new ArgumentException(
                    $"Product wit id {productId} does not exist",
                    nameof(product));
            }

            return _mapper.Map<ProductDetailsDTO>(product);
        }

        public async Task<IEnumerable<ProductShortDTO>> GetProductListAsync(
            long? categoryId)
        {
            var products = categoryId == null
                ? await _productRepository.GetAsync()
                : await _productRepository.GetByCategoryAsync(categoryId);

            return _mapper.Map<IEnumerable<ProductShortDTO>>(products);
        }
    }
}
