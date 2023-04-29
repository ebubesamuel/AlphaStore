using System.Threading.Tasks;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Services.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsCategoryUniqueAsync(
            long? categoryId,
            string name);
    }
}


