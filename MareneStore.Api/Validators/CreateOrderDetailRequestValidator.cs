using FluentValidation;
using MareneStore.Api.Requests;

namespace MareneStore.Api.Validators
{
    public class CreateOrderDetailRequestValidator : AbstractValidator<OrderDetailRequest>
    {
        public CreateOrderDetailRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is required.")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}