using FluentValidation;
using MareneStore.Api.Requests;

namespace MareneStore.Api.Validators
{
    public class UpdateProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Product name cannot be empty.")
                    .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
            });

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");
            });

            When(x => x.Price != null, () =>
            {
                RuleFor(x => x.Price)
                    .GreaterThan(0).WithMessage("Product price must be greater than zero.");
            });

            When(x => x.Stock != null, () =>
            {
                RuleFor(x => x.Stock)
                    .GreaterThanOrEqualTo(0).WithMessage("Product stock cannot be negative.");
            });

            When(x => x.ImageUrl != null, () =>
            {
                RuleFor(x => x.ImageUrl)
                    .MaximumLength(250).WithMessage("Image URL cannot exceed 250 characters.");
            });

            When(x => x.IsActive != null, () =>
            {
                RuleFor(x => x.IsActive)
                    .Must(isActive => isActive == true || isActive == false)
                    .WithMessage("Product active status must be a boolean value.");
            });
        }
    }
}
