using System.Net;
using VaccinationCard.Api.Responses;

namespace VaccinationCard.Api.Middlewares
{
    /// <summary>
    /// Handles exceptions thrown by the application
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var apiResponse = new ApiResponse(false, "Erro ao realizar operação", HttpStatusCode.InternalServerError);

                await context.Response.WriteAsJsonAsync(apiResponse);
            }
        }
    }

}
