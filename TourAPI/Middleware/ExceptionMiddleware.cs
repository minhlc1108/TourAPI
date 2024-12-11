using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TourAPI.Dtos.Error;
using Microsoft.AspNetCore.Http;
using TourAPI.Exceptions;

namespace TourAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            ErrorResponseDto errorDto = new ErrorResponseDto()
            {
                Message = exception.Message,
            };

            int statusCode;

            switch (exception)
            {
                case InvalidOperationException:
                    statusCode = (int)HttpStatusCode.Conflict;
                    break;
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BadHttpRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    errorDto.Message = "Có lỗi xảy ra...";
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            errorDto.StackTrace = exception.StackTrace;
            errorDto.Status = statusCode;
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorDto, options));
        }
    }
}