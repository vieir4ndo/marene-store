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
    public class StockRepository : IStockRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<Stock> _dbSet;

        public StockRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Stock>();
        }

        public async Task<Stock> AddAsync(Stock entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Stock>> FindAsync(Expression<Func<Stock, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _dbSet
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Stock entity)
        {
            _dbSet.Remove(entity);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Stock entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
