using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace StokTakipOtomasyon.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                // Log this exception
                _logger.LogError(e, e.Message);

                // Return a generalized custom error response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problemDetails = new ProblemDetails
                {
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server has occurred"
                };

                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
