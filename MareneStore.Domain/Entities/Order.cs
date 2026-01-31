using MareneStore.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MareneStore.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderDetail> Details { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserShippingAddress { get; set; }
    }
}
