
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaStore.Application.Services.Interfaces;
using AlphaStore.Application.Services.Interfaces.Services;
using AlphaStore.Application.Models;
using AlphaStore.Application.Services.DTO;

namespace AlphaStore.Application.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(
            IMapper mapper,
            IProductRepository productRepository,
            IShoppingCartRepository shoppingCartRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<long?> CreateShoppingCart(long? productId, int quantity)
        {
            if (productId is null || productId <= 0)
            {
                throw new ArgumentException(
                    $"Invalid product id {productId}");
            }

            var product = await _productRepository.GetAsync(productId);

            if (product == null)
            {
                throw new ArgumentException(
                    $"Invalid product id {productId}. " +
                    $"Product not found.");
            }

            var shoppingCart = await _shoppingCartRepository.GetPending();

            if (shoppingCart is null)
            {
                shoppingCart = ShoppingCart.Create(
                    product.Price.Currency);
            }

            shoppingCart.AddItem(
                product,
                quantity);

            if (shoppingCart.Id is null)
            {
                await _shoppingCartRepository.SaveAsync(shoppingCart);
            }
            else
            {
                await _shoppingCartRepository.UpdateAsync(shoppingCart);
            }

            return shoppingCart.Id;
        }

        public async Task<ShoppingCartDTO> GetShoppingCartDetailsAsync(long? shoppingCartId)
        {
            if (shoppingCartId is null || shoppingCartId <= 0)
            {
                throw new ArgumentException(
                    $"Invalid shopping cart id {shoppingCartId}.");
            }

            var shoppingCart = await _shoppingCartRepository.GetAsync(shoppingCartId);

            if (shoppingCart is null)
            {
                throw new ArgumentException(
                    $"Invalid shopping cart id {shoppingCartId}. " +
                    $"Shopping cart not found.");
            }

            return _mapper.Map<ShoppingCartDTO>(shoppingCart);
        }

        public async Task<IEnumerable<ShoppingCartShortDTO>> GetShoppingCartListAsync()
        {
            var shoppingCarts = await _shoppingCartRepository.GetAsync();

            return _mapper.Map<IEnumerable<ShoppingCartShortDTO>>(shoppingCarts);
        }
    }
}
