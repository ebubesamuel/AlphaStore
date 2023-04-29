using System.Threading.Tasks;
using AlphaStore.Application.Models;
using AlphaStore.Application.Services.Interfaces;

namespace AlphaStore.Application.Services.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetPending();
    }
}
