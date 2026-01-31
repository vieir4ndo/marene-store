using System;
using System.Collections.Generic;

namespace MareneStore.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
