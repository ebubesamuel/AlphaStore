using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Services.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(long? categoryId);
        Task<bool> IsProductUniqueAsync(string name);
        Task<bool> IsProductUniqueAsync(string name, long? productId);
    }
}
