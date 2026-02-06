using MareneStore.Domain.Enums;
using System.Linq;

namespace MareneStore.Api.Requests
{
    public class UpdateOrderStatusRequest
    {
        public OrderStatus Status { get; set; }

        public void Validate()
        {
            var validator = new Validators.UpdateOrderStatusRequestValidator();
            var validationResult = validator.Validate(this);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new Domain.Exceptions.ValidationException(errors);
            }
        }
    }
}