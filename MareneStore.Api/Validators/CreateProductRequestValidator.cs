using FluentValidation;
using MareneStore.Api.Requests;

namespace MareneStore.Api.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name cannot exceed 100 characters.");
            
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Product description cannot exceed 500 characters.");
            
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Product price is required.")
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.Stock)
                .NotEmpty()
                .WithMessage("Product stock is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product stock cannot be negative.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(250)
                .WithMessage("Image URL cannot exceed 200 characters.");

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage("Product active status is required.");
        }
    }
}