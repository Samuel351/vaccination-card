using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Api.Responses;

namespace VaccinationCard.Api.Extensions
{
    public static class ResultExtension 
    {
        public static IActionResult HandleResult(this ControllerBase controller, Result result)
        {
            ApiResponse? apiResponse;
            if (result.IsSuccess)
            {
                apiResponse = new ApiResponse(result.IsSuccess, "", result.StatusCode);
            }
            else
            {
                apiResponse = new ApiResponse(result.IsSuccess, result.Error?.Description ?? string.Empty, result.StatusCode);
            }

            return controller.StatusCode(apiResponse.StatusCode, apiResponse);
        }

        public static IActionResult HandleResult<T>(this ControllerBase controller, Result<T> result)
        {
            if(!result.IsSuccess)
            {
                ApiResponse apiResponse = new ApiResponse(result.IsFailure, result.Error?.Description ?? string.Empty, result.StatusCode);

                return controller.StatusCode(apiResponse.StatusCode, apiResponse);
            }
            return controller.StatusCode((int)result.StatusCode, result.Value);
        }
    }
}
