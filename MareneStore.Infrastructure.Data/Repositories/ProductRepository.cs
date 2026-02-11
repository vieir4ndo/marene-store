using MareneStore.Domain.Entities;
using MareneStore.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MareneStore.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<Product> _dbSet;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _dbSet
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<Product> AddAsync(Product entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        public void Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Product entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<int> CountAsync()
        {
            return _dbSet.CountAsync();
        }
    }
}