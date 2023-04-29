using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaStore.Application.Services.DTO;

namespace AlphaStore.Application.Services.Interfaces.Services
{
    public interface IShoppingCartService
    {
        Task<long?> CreateShoppingCart(
            long? productId,
            int quantity);

        Task<ShoppingCartDTO> GetShoppingCartDetailsAsync(
            long? shoppingCartId);

        Task<IEnumerable<ShoppingCartShortDTO>> GetShoppingCartListAsync();
    }
}
