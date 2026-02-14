using MareneStore.Application.Dtos;
using MareneStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> GetByIdAsync(Guid id);
        Task<(List<OrderDto>, int)> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<OrderDto> UpdateOrderStatusAsync(Guid id, OrderStatus status, string userEmail = null);
        Task<(List<OrderDto>, int)> GetOrdersByUserEmailAsync(string email, int pageNumber = 1, int pageSize = 10);
    }
}
