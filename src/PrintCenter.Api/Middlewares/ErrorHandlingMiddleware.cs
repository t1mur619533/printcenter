using System;
using System.Net;
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
                case RestException restException:
                    errors = restException.Errors;
                    context.Response.StatusCode = (int) restException.Code;
                    break;
                case ValidationException validationException:
                    errors = validationException.Errors;
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                case { } e:
                    logger.LogError(string.IsNullOrWhiteSpace(e.Message) ? e.ToString() : e.Message);
                    errors = "Произошла непредвиденная ошибка, пожалуйста, свяжитесь с администратором.";
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
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