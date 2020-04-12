using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using c03.Models;
using Microsoft.AspNetCore.Http;

namespace c03.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "applications/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsync(
                new ErrorDetails 
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error occured: {exception}"
                }.ToString()
            );
        }
    }
}