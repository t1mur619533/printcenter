using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PrintCenter.Domain.Exceptions;

namespace PrintCenter.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            object errors = null;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    errors = notFoundException.Message;
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case DuplicateException duplicateException:
                    errors = duplicateException.Message;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case InvalidArgumentException invalidArgumentException:
                    errors = invalidArgumentException.Message;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case AccessDeniedException accessDeniedException:
                    errors = accessDeniedException.Message;
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    break;
                case ValidationException validationException:
                    errors = validationException.Errors;
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    break;
                case { } e:
                    logger.LogError(string.IsNullOrWhiteSpace(e.Message) ? e.ToString() : e.Message);
                    errors =
                        "The server encountered an internal error or misconfiguration and was unable to complete your request. Please contact the server administrator.";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(new
            {
                errors
            });

            await context.Response.WriteAsync(result);
        }
    }
}