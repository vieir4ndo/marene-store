using FluentValidation;
using MareneStore.Api.Requests;

namespace MareneStore.Api.Validators
{
    public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>
    {

        public UpdateOrderStatusRequestValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.")
                .IsInEnum()
                .WithMessage("Invalid status value.");
        }
    }
}