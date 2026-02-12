using FluentValidation;
using MareneStore.Api.Requests;

namespace MareneStore.Api.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.UserShippingAddress)
            .NotEmpty()
            .WithMessage("UserShippingAddress is required.");

            RuleForEach(x => x.Details)
                .SetValidator(new CreateOrderDetailRequestValidator());
        }
    }
}