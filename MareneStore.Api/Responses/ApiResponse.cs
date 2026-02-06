using Newtonsoft.Json;
using System.Collections.Generic;

namespace MareneStore.Api.Responses
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
        public int? PageNumber { get; set; } = null;
        public int? PageSize { get; set; } = null;

        public static ApiResponse CreateSuccess(object data)
        {
            return new ApiResponse()
            {
                Success = true,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse CreateSuccess(object data, int pageNumber, int pageSize)
        {
            return new ApiResponse()
            {
                Success = true,
                Data = data,
                Errors = null,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public static ApiResponse CreateError(List<string> errors)
        {
            return new ApiResponse()
            {
                Success = false,
                Data = null,
                Errors = errors
            };
        }
    }
}