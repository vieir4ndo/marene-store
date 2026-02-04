using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> GetByIdAsync(Guid id);
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto> CancelOrderAsync(Guid id);
    }
}
