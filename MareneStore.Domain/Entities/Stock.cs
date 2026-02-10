using System;

namespace MareneStore.Domain.Entities
{
    public class Stock
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        protected Stock() { }

        public Stock(Guid productId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
        }
    }
}
