using MareneStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MareneStore.Infrastructure.Data.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Stock> GetByIdAsync(Guid id);
        Task<IEnumerable<Stock>> FindAsync(Expression<Func<Stock, bool>> predicate);
        Task<Stock> AddAsync(Stock entity);
        void Update(Stock entity);
        void Remove(Stock entity);
        Task<int> SaveChangesAsync();
    }
}
