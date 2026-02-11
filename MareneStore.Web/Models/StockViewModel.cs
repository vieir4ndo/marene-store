using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;

namespace MareneStore.Web.Models
{
    public class StockViewModel
    {
        public List<StockItemViewModel> Items { get; set; }

        public static StockViewModel FromDto(List<StockDto> dtos)
        {
            return new StockViewModel
            {
                Items = dtos.ConvertAll(dto => new StockItemViewModel
                {
                    ProductName = dto.ProductName,
                    ProductQuantity = dto.ProductQuantity,
                    ImageUrl = dto.ImageUrl,
                    ProductId = dto.ProductId
                })
            };
        }

        public class StockItemViewModel
        {
            public string ProductName { get; set; }
            public int ProductQuantity { get; set; }
            public string ImageUrl { get; set; }
            public Guid ProductId { get; set; }
        }
    }
}