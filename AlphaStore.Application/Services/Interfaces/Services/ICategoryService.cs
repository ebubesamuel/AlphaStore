using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaStore.Application.Services.DTO;

namespace AlphaStore.Application.Services.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<long?> CreateCategoryAsync(CreateUpdateCategoryDTO createCategory);
        Task DeleteCategoryAsync(long? categoryId);
        Task<IEnumerable<CategoryShortDTO>> GetCategoryListAsync();
        Task UpdateCategoryAsync(long? categoryId, CreateUpdateCategoryDTO updateCategory);
    }
}