using AlphaStore.Application.Models;
using AlphaStore.Application.Services.Interfaces;
using AlphaStore.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaStore.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DatabaseContext _context;

        public ShoppingCartRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task DeleteAsync(ShoppingCart entity)
        {
            _context.ShoppingCarts.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<ShoppingCart> GetAsync(long? id)
        {
            return await _context.ShoppingCarts.AsQueryable()
                .Include(e => e.Items).ThenInclude(e => e.Product).ThenInclude(e => e.Price)
                .Where(e => e.Id == id)
                .SingleAsync();
        }

        public async Task<IEnumerable<ShoppingCart>> GetAsync()
        {
            return await _context.ShoppingCarts.AsQueryable()
                .Include(e => e.Items).ThenInclude(e => e.Product).ThenInclude(e => e.Price)
                .ToListAsync();
        }

        public async Task<long?> SaveAsync(ShoppingCart entity)
        {
            await _context.ShoppingCarts.AddAsync(entity);
            return entity.Id;
        }

        public Task UpdateAsync(ShoppingCart entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public Task<ShoppingCart> GetPending()
        {
            return _context.ShoppingCarts.AsQueryable()
                .Include(e => e.Items).ThenInclude(e => e.Product).ThenInclude(e => e.Price)
                .Where(e => e.Status == ShoppingCartStatus.Pending)
                .SingleOrDefaultAsync();
        }
    }
}
