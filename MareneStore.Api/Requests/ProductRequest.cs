using FluentValidation.Results;
using MareneStore.Api.Validators;
using MareneStore.Application.Dtos;
using System.Linq;

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

        public void Validate()
        {
            var validationResult = new ProductRequestValidator().Validate(this);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new Domain.Exceptions.ValidationException(errors);
            }
        }
    }
}