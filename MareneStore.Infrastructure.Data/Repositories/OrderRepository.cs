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
    public class OrderRepository : IOrderRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<Order> _dbSet;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Order>();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _dbSet
                .OrderBy(x=>x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(o=>o.Details)
            .Include(o => o.Details.Select(d => d.Product))
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<Order> AddAsync(Order entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        public void Update(Order entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Order entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
