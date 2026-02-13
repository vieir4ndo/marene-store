using MareneStore.Application.Dtos;
using System.Collections.Generic;

namespace MareneStore.Api.Requests
{
    public class OrderRequest
    {
        public string UserShippingAddress { get; set; }
        public List<OrderDetailRequest> Details { get; set; }

        public void ValidateCreation()
        {
            var validationResult = new Validators.CreateOrderRequestValidator().Validate(this);
            if (!validationResult.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                throw new Domain.Exceptions.ValidationException(errors);
            }
        }

        public OrderDto ToDto(string userEmail, string userName)
        {
            return new OrderDto
            {
                UserName = userName,
                UserEmail = userEmail,
                UserShippingAddress = this.UserShippingAddress,
                Details = this.Details.ConvertAll(d => d.ToDto())
            };
        }
    }
}