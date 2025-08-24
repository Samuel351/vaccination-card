using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace VaccinationCard.Api.Extensions
{
    public static class ResultExtension 
    {
        public static IActionResult HandleResult(this ControllerBase controller, Result result)
        {
            return controller.StatusCode((int)result.StatusCode, result.IsSuccess ? null : result.Error);
        }

        public static IActionResult HandleResult<T>(this ControllerBase controller, Result<T> result)
        {
            return controller.StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }
    }
}
