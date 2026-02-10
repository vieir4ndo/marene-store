using MareneStore.Domain.Entities;
using System;

namespace MareneStore.Application.Dtos
{
    public class StockDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }

        public static StockDto FromEntity(Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                ProductQuantity = stock.Product.Stock
            };
        }
    }
}
