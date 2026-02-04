using MareneStore.Domain.Entities;
using System;

namespace MareneStore.Application.Dtos
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public static OrderDetailDto FromEntity(OrderDetail orderDetail)
        {
            return new OrderDetailDto
            {
                Id = orderDetail.Id,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };
        }
    }
}
