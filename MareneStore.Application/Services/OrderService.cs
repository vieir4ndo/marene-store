using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Entities;
using MareneStore.Domain.Enums;
using MareneStore.Domain.Exceptions;
using MareneStore.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MareneStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IStockService _stockService;

        public OrderService(IOrderRepository orderRepository, IProductService productService, IStockService stockService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _stockService = stockService;
        }

        public async Task<OrderDto> UpdateOrderStatusAsync(Guid id, OrderStatus status, string userEmail = null)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new Domain.Exceptions.NotFoundException();
            }

            if (userEmail != null && order.UserEmail != userEmail)
            {
                throw new ConflictException($"User {userEmail} cannot update this order because it belongs to another user");
            }

            var (success, errorMessage) = order.UpdateStatus(status);

            if (!success)
            {
                throw new Domain.Exceptions.ConflictException(errorMessage);
            }

            if (status == OrderStatus.Cancelled)
            {
                foreach (Product p in order.Details.Select(d => d.Product).ToList())
                {
                    var orderedQuantity = order.Details.First(od => od.ProductId == p.Id).Quantity;
                    p.Stock += orderedQuantity;
                    if (p.Stock > 0)
                    {
                        p.IsActive = true;
                    }
                }
            }

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();

            return OrderDto.FromEntity(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var products = await _productService.GetActiveProductsByIdsAsync(orderDto.Details.Select(x => x.ProductId).ToList());

            if (products.Count != orderDto.Details.Count)
            {
                throw new Domain.Exceptions.ValidationException("One or more products were not found");
            }

            var details = orderDto.Details.Select(od =>
            {
                var product = products.First(p => p.Id == od.ProductId);
                if (product.Stock < od.Quantity)
                {
                    throw new Domain.Exceptions.ValidationException($"Insufficient stock for product {product.Name}");
                }
                return new OrderDetail(product.Id, product.Price.Value, od.Quantity);
            }).ToList();

            var orderEntity = new Order(orderDto.UserName, orderDto.UserEmail, orderDto.UserShippingAddress, details);

            var createdOrder = await _orderRepository.AddAsync(orderEntity);
            foreach (ProductDto p in products)
            {
                var orderedQuantity = orderDto.Details.First(od => od.ProductId == p.Id).Quantity;
                p.Stock -= orderedQuantity;

                if (p.Stock == 0)
                {
                    p.IsActive = false;
                }

                if (p.Stock <= 2)
                {
                    await _stockService.CreateStockAsync(p.Id);
                }

                await _productService.UpdateProductAsync(p);
            }

            return OrderDto.FromEntity(createdOrder);
        }

        public async Task<(List<OrderDto>, int)> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var orders = await _orderRepository.GetAllAsync(pageNumber, pageSize);
            var totalCount = await _orderRepository.GetTotalCountAsync();
            return (orders.ToList().ConvertAll(OrderDto.FromEntity), totalCount);
        }

        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new Domain.Exceptions.NotFoundException();
            }

            return OrderDto.FromEntity(order);
        }

        public async Task<(List<OrderDto>, int)> GetOrdersByUserEmailAsync(string email, int pageNumber = 1, int pageSize = 10)
        {
            var (orders, totalCount) = await _orderRepository.GetByUserEmailAsync(email, pageNumber, pageSize);

            return (orders.ToList().ConvertAll(OrderDto.FromEntity), totalCount);
        }
    }
}
