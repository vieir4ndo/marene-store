using System;

namespace MareneStore.Domain.Entities
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        protected OrderDetail() { }

        public OrderDetail(Guid productId, decimal unitPrice, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
