using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;

namespace MareneStore.Web.Models
{
    public class ProductViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } // Added TotalCount property
        public List<ProductItemViewModel> Items { get; set; }

        public static ProductViewModel FromDto(List<ProductDto> dtos, int pageNumber, int pageSize, int totalCount)
        {
            return new ProductViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = dtos.ConvertAll(dto => new ProductItemViewModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price ?? 0,
                    Stock = dto.Stock ?? 0,
                    IsActive = dto.IsActive ?? false,
                    ImageUrl = dto.ImageUrl
                })
            };
        }

        public class ProductItemViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public bool IsActive { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}