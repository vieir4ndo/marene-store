using MareneStore.Application.Dtos;
using MareneStore.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MareneStore.Web.Models
{
    public class OrderViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<OrderItemViewModel> Items { get; set; }

        public static OrderViewModel FromDto(List<OrderDto> dtos, int pageNumber, int pageSize, int totalCount)
        {
            return new OrderViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = dtos.ConvertAll(dto => new OrderItemViewModel
                {
                    Id = dto.Id,
                    Date = dto.Date,
                    TotalAmount = dto.TotalAmount,
                    Status = dto.Status,
                    UserShippingAddress = dto.UserShippingAddress,
                    UserEmail = dto.UserEmail
                })
            };
        }

        public class OrderItemViewModel
        {
            public Guid Id { get; set; }
            public DateTime Date { get; set; }
            public decimal TotalAmount { get; set; }
            public OrderStatus Status { get; set; }
            public string UserShippingAddress { get; set; }
            public string UserEmail { get; set; }
        }
    }
}