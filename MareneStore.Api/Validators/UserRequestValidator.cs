using FluentValidation;

namespace MareneStore.Api.Validators
{
    public class UserRequestValidator : FluentValidation.AbstractValidator<Requests.UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .EmailAddress().WithMessage("Username must be a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}