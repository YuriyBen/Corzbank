using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private ILoggerManager logger;

        public ExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                logger = scope.ServiceProvider.GetRequiredService<ILoggerManager>(); 

                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Something went wrong: {ex}");
                    await HandleExceptionAsync(httpContext, ex);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ErrorDetailsModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
