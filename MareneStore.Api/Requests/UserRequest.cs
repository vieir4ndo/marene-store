using MareneStore.Api.Validators;
using MareneStore.Domain.Exceptions;
using System.Linq;

namespace MareneStore.Api.Requests
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            var result = new UserRequestValidator().Validate(this);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}