using MareneStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MareneStore.Infrastructure.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<Product> AddAsync(Product entity);
        void Update(Product entity);
        void Remove(Product entity);
        Task<int> SaveChangesAsync();
    }
}
