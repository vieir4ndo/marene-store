using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MareneStore.Domain.Entities;

namespace MareneStore.Web.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }

        public class ShoppingCartItem
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public int Quantity { get; set; }
        }
    }
}