using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dtos;

namespace Sdk.Api.Utils
{
    public static class ApiActionResult
    {
        public static ActionResult Ok<T>(T value)
        {
            return new OkObjectResult(new ApiResponseDto<T> { Data = value });
        }

        public static ActionResult Created<T>(T value)
        {
            return new CreatedResult(string.Empty, new ApiResponseDto<T> { Data = value });
        }
    }
}
