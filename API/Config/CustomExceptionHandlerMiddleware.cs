using System;
using System.Net;
using System.Threading.Tasks;

using BookArchive.Application;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace BookArchive.API {
    public class CustomExceptionHandlerMiddleware {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) {
            int code = (int) HttpStatusCode.InternalServerError;
            
            var result = exception?.ToString();

            result = JsonConvert.SerializeObject(
                new CQRSResult<string>(
                    data: null,
                    code: 500,
                    message: "Internal error",
                    errors : result ,
                    hasError : true,
                    wasHandledError : false));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder) {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

}