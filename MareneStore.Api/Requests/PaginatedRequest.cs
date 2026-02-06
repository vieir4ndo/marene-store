using MareneStore.Domain.Exceptions;
using System.Collections.Generic;

namespace MareneStore.Api.Requests
{
    public class PaginatedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public void Validate()
        {
            List<string> errors = new List<string>();
            if (PageNumber <= 0)
            {
                errors.Add("Page number must be greater than 0.");
            }

            if (PageSize <= 0 || PageSize > 100)
            {
                errors.Add("Page size must be between 1 and 100.");
            }

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}