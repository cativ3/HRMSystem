using HRMSystem.Core.Utilities.Exceptions;
using HRMSystem.Core.Utilities.Results.ComplexTypes;
using HRMSystem.Core.Utilities.Results.Concretes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace HRMSystem.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case ArgumentNotFoundException error:
                        await ArgumentNotFoundAsync(context, error);
                        break;
                    case ValidationErrorException error:
                        await ValidationErrorAsync(context, error);
                        break;
                    default:
                        await GeneralExceptionAsync(context, exception);
                        break;
                }
            }
        }

        private async Task ValidationErrorAsync(HttpContext context, ValidationErrorException exception)
        {
            IEnumerable<Error> errors = exception.Errors;

            var badResult = new Result(
                ResultStatus.Warning,
                "One or more errors occured.",
                errors);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var newResponse = JsonSerializer.Serialize(badResult);

            await context.Response.WriteAsync(newResponse);
        }

        private async Task ArgumentNotFoundAsync(HttpContext context, ArgumentNotFoundException exception)
        {
            IEnumerable<Error> errors = exception.Errors;

            var badResult = new Result(
                ResultStatus.Warning, 
                "One or more errors occured.",
                errors);

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            var newResponse = JsonSerializer.Serialize(badResult);

            await context.Response.WriteAsync(newResponse);
        }

        private async Task GeneralExceptionAsync(HttpContext context, Exception exception)
        {
            var badResult = new
            {
                ResultStatus = ResultStatus.Error,
                Message = exception.Message,
                Detail = exception.StackTrace
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var newResponse = JsonSerializer.Serialize(badResult);

            await context.Response.WriteAsync(newResponse);

        }
    }
}
