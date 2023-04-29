using AlphaStore.Application.Models;
using AlphaStore.Application.Services.Interfaces;
using AlphaStore.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaStore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task DeleteAsync(Product entity)
        {
            _context.Products.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<Product> GetAsync(long? id)
        {
            return await _context.Products.AsQueryable()
                .Include(e => e.Price)
                .Include(e => e.Category)
                .Where(e => e.Id == id)
                .SingleAsync();
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _context.Products.AsQueryable()
                .Include(e => e.Price)
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(long? categoryId)
        {
            return await _context.Products.AsQueryable()
                .Include(e => e.Price)
                .Include(e => e.Category)
                .Where(e => e.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<bool> IsProductUniqueAsync(string name)
        {
            var exists = await _context.Products.AsQueryable()
                .Where(e => e.Name == name)
                .Select(e => true)
                .AnyAsync();

            return !exists;
        }

        public async Task<bool> IsProductUniqueAsync(
            string name,
            long? productId)
        {
            var exists = await _context.Products.AsQueryable()
                .Where(e => e.Name == name)
                .Where(e => e.Id != productId)
                .Select(e => true)
                .AnyAsync();

            return !exists;
        }

        public async Task<long?> SaveAsync(Product entity)
        {
            var data = await _context.Products.AddAsync(entity);

            return entity.Id;
        }

        public Task UpdateAsync(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }
    }
}
