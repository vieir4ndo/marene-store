using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IStockService
    {
        Task<StockDto> CreateStockAsync(Guid productId);
        Task<List<StockDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task RemoveByProductIdAsync(Guid productId);
    }
}
