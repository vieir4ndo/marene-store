using MareneStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MareneStore.Web.Models
{
    public class ProductViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
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
            public Guid? Id { get; set; }

            [Required(ErrorMessage = "Product name is required.")]
            [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
            public string Name { get; set; }

            [StringLength(500, ErrorMessage = "Product description cannot exceed 500 characters.")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Product price is required.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero.")]
            public decimal Price { get; set; }

            [Required(ErrorMessage = "Product stock is required.")]
            [Range(0, int.MaxValue, ErrorMessage = "Product stock cannot be negative.")]
            public int Stock { get; set; }

            [Required(ErrorMessage = "Product active status is required.")]
            public bool IsActive { get; set; }

            [Required(ErrorMessage = "ImageUrl is required.")]
            [StringLength(250, ErrorMessage = "Image URL cannot exceed 250 characters.")]
            [Url(ErrorMessage = "Invalid URL format.")]
            public string ImageUrl { get; set; }
        }
    }
}