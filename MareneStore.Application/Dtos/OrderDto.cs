using MareneStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MareneStore.Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public virtual List<OrderDetailDto> Details { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserShippingAddress { get; set; }

        public static OrderDto FromEntity(Domain.Entities.Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Date = order.Date,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserName = order.UserName,
                UserEmail = order.UserEmail,
                UserShippingAddress = order.UserShippingAddress,
                Details = order.Details != null ? 
                    order.Details.ToList().ConvertAll(od => OrderDetailDto.FromEntity(od))
                    : new List<OrderDetailDto>()
            };
        }
    }
}
