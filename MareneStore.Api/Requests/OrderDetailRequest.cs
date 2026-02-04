using MareneStore.Application.Dtos;
using System;

namespace MareneStore.Api.Requests
{
    public class OrderDetailRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public OrderDetailDto ToDto()
        {
            return new OrderDetailDto
            {
                ProductId = this.ProductId,
                Quantity = this.Quantity
            };
        }
    }
}