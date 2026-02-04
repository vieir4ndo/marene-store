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

        protected Order() { }

        public Order(string userName, string userEmail, string userShippingAddress, List<OrderDetail> details)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            UserEmail = userEmail;
            UserShippingAddress = userShippingAddress;
            Date = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            Details = details;
            TotalAmount = details != null ? CalculateTotalAmount(details) : 0;
        }

        private static decimal CalculateTotalAmount(List<OrderDetail> details)
        {
            decimal total = 0;
            foreach (var detail in details)
            {
                total += detail.UnitPrice * detail.Quantity;
            }
            return total;
        }

        public bool CancelOrder()
        {
            if (Status == OrderStatus.Pending)
            {
                Status = OrderStatus.Cancelled;
                return true;
            }
            return false;
        }
    }
}
