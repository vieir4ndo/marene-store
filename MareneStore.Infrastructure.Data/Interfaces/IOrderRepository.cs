using MareneStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MareneStore.Infrastructure.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate);
        Task<Order> AddAsync(Order entity);
        void Update(Order entity);
        void Remove(Order entity);
        Task<int> SaveChangesAsync();
        Task<int> GetTotalCountAsync();
        Task<(IEnumerable<Order>, int)> GetByUserEmailAsync(string email, int pageNumber = 1, int pageSize = 10);
    }
}
