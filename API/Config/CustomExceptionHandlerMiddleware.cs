using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using BookArchive.Application;

namespace BookArchive.API
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int code = (int)HttpStatusCode.InternalServerError;

            var result = exception?.ToString();

            if (exception is CQRSValidationException)
            {
                code = (int)HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject((exception as CQRSValidationException).Data);
            }
            else
            {
                result = JsonConvert.SerializeObject(new CQRSResult<string>(result, 500, "Internal error", true, false));
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

}
