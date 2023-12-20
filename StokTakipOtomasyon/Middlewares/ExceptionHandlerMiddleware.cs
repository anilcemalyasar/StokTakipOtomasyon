using Microsoft.AspNetCore.Mvc;
using StokTakipOtomasyon.Exceptions;
using StokTakipOtomasyon.Models.Domain;
using System.Net;
using System.Text.Json;

namespace StokTakipOtomasyon.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        // private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            // _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                // Log this exception
                _logger.LogError(error, error.Message);

                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ApiResponse<string>.Fail(error.Message);
                switch (error)
                {
                    case ProductNotFoundException 
                        or CompanyNotFoundException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);

            }
        }
    }
}
