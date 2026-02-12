using MareneStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MareneStore.Infrastructure.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<Product> AddAsync(Product entity);
        void Update(Product entity);
        void Remove(Product entity);
        Task<int> SaveChangesAsync();
        Task<int> GetTotalCountAsync();
    }
}
