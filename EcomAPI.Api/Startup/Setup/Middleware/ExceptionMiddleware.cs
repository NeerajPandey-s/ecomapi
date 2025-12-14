using EcomAPI.Common;
using EcomAPI.Common.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static EcomAPI.Api.Startup.ActionFilters.ApiResponseFilter;

namespace EcomAPI.Api.Startup.Setup.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                var userTimeZone = httpContext.Request.Headers["user-time-zone"];
                UserDatetimeOffSetDetails.UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById(string.IsNullOrEmpty(userTimeZone) ? "UTC" : Convert.ToString(userTimeZone));

                await _next(httpContext);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                await HandleException(httpContext, dbEx);
            }
            catch (ForbiddenAccessException ex)
            {
                await HandleException(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            Guid guid = Guid.NewGuid();
            context.Response.ContentType = "application/json";

            if (context.Response.StatusCode != 403)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            string errorMessage = $"{exception.Message}. ErrorId: {guid}.";

            
            using (var writer = new StreamWriter(context.Response.Body))
            {
                var errorResponse = new ErrorResponse
                {
                    ErrorMessage = errorMessage
                };
                await writer.WriteAsync(JsonSerializer.Serialize(new ApiResponse<ErrorResponse>(false, "Failure!", errorResponse)));
                await writer.FlushAsync();
            }

            LogException(exception, guid, errorMessage, NLog.LogLevel.Error);
            await context.Response.WriteAsync("");
        }


        private async Task HandleException(HttpContext context, Microsoft.EntityFrameworkCore.DbUpdateException exception)
        {
            Guid guid = Guid.NewGuid();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string errorMessage = $@"{exception.Message}
{exception.InnerException?.Message}.
ErrorId: {guid}.";


            using (var writer = new StreamWriter(context.Response.Body))
            {
                var errorResponse = new ErrorResponse
                {
                    ErrorMessage = errorMessage
                };
                await writer.WriteAsync(JsonSerializer.Serialize(new ApiResponse<ErrorResponse>(false, "Failure!", errorResponse)));
                await writer.FlushAsync();
            }

            LogException(exception, guid, errorMessage, NLog.LogLevel.Error);
            await context.Response.WriteAsync("");
        }

        private async Task HandleException(HttpContext context, ForbiddenAccessException exception)
        {
            Guid guid = Guid.NewGuid();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            string errorMessage = $@"Unauthorized: {exception.Message}";

            using (var writer = new StreamWriter(context.Response.Body))
            {
                var errorResponse = new ErrorResponse
                {
                    ErrorMessage = errorMessage
                };
                await writer.WriteAsync(JsonSerializer.Serialize(new ApiResponse<ErrorResponse>(false, "Failure!", errorResponse)));
                await writer.FlushAsync();
            }

            LogException(exception, guid, errorMessage, NLog.LogLevel.Error);
            await context.Response.WriteAsync("");
        }

        private void LogException(Exception exception, Guid messageId, string message, NLog.LogLevel logLevel)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            NLog.LogEventInfo info = new(logLevel, exception?.TargetSite?.ReflectedType?.FullName ?? "", message);

            info.Properties["messageId"] = $"{messageId}";

            info.Exception = exception;
            logger.Log(info);
        }

        public record ErrorResponse()
        {
            public required string ErrorMessage { get; set; }
        }
    }
}
