using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using ExamApp.Domain.Exceptions;
using ExamApp.Models.Dto;

namespace ExamApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;        
        public ExceptionMiddleware(RequestDelegate next)
        {           
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException ex)
            {                
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }

        private async Task HandleExceptionAsync(HttpContext context, BusinessException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = exception switch
            {
                BusinessException => exception.Message,
                _ => "Error from the supplier portal business logic"
            };

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                InternalErrorCode = exception.ErrorCode,
                Message = message
            }.ToString());
        }
    }
}
