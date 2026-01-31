using FluentValidation.Results;
using MareneStore.Api.Validators;
using MareneStore.Application.Dtos;

namespace MareneStore.Api.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }

        public ProductDto ToDto()
        {
            return new ProductDto
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Stock = this.Stock,
                IsActive = this.IsActive,
                ImageUrl = this.ImageUrl
            };
        }

        public ValidationResult Validate()
        {
            return new ProductRequestValidator().Validate(this);
        }
    }
}