using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;

namespace MareneStore.Web.Models
{
    public class StockViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<StockItemViewModel> Items { get; set; }

        public static StockViewModel FromDto(List<StockDto> dtos, int pageNumber, int pageSize, int totalCount)
        {
            return new StockViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
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