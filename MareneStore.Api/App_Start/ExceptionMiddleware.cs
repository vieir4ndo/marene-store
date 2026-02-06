﻿using MareneStore.Api.Responses;
using MareneStore.Domain.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MareneStore.Api.App_Start
{
    public class ExceptionMiddleware : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var ex = context.Exception;

            if (ex is ValidationException vex)
            {
                context.Response = context.Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new ApiResponse()
                {
                    Success = false,
                    Data = null,
                    Errors = vex.Errors
                });
            }
            else if (ex is NotFoundException nfx)
            {
                context.Response = context.Request.CreateResponse(
                HttpStatusCode.NotFound,
                new ApiResponse()
                {
                    Success = false,
                    Data = null,
                    Errors = null
                });
            }
            else if (ex is ConflictException cfx)
            {
                context.Response = context.Request.CreateResponse(
                HttpStatusCode.Conflict,
                new ApiResponse()
                {
                    Success = false,
                    Data = null,
                    Errors = new System.Collections.Generic.List<string>() { cfx.Message }
                });
            }
            else
            {
                context.Response = context.Request.CreateResponse(
                HttpStatusCode.NotFound,
                new ApiResponse()
                {
                    Success = false,
                    Data = null,
                    Errors = new System.Collections.Generic.List<string>() { "An unexpected error occurred." }
                });
            }
        }
    }
}