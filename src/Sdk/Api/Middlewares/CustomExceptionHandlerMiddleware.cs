using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sdk.Api.Dtos;
using Sdk.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Api.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            List<Error> errors = new List<Error>();
            var code = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case CustomException customException:
                    {
                        code = customException.StatusCode;
                        errors.Add(new Error(ExceptionType.Custom, null, customException.Message));
                        break;
                    }
                case ValidationException validationException:
                    {
                        code = HttpStatusCode.BadRequest;
                        errors.AddRange(validationException.Failures
                            .Select(f => new Error(ExceptionType.Validation, f.Key, f.Value))
                            .ToList());
                        break;
                    }
                case Exception exception:
                    {
                        errors.Add(new Error(ExceptionType.System, "system", exception.Message));
                        break;
                    }
            }

            await BuildApiResponseDto(context, new ApiResponseDto
            {
                Errors = errors
            }, (int) code);
        }

        private static Task BuildApiResponseDto(HttpContext context, ApiResponseDto apiResponseDto,
            int statusCode)
        {
            string response = JsonConvert.SerializeObject(apiResponseDto);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(response, Encoding.UTF8);
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
