using EvenDriven.Domain.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace EvenDriven.Api.Extensions
{
    public static class ResultExtension
    {
        public static IResult ToHttpResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }

            return MapErrorToHttpResult(result.Error);
        }

        private static IResult MapErrorToHttpResult(Error error)
        {
            return error.Type switch
            {
                ErrorType.Validation => Results.Problem(detail: error.Message, statusCode: StatusCodes.Status400BadRequest, title: "Validation Error"),
                ErrorType.NotFound => Results.Problem(detail: error.Message, statusCode: StatusCodes.Status404NotFound, title: "Resource Not Found"),
                ErrorType.Conflict => Results.Problem(detail: error.Message, statusCode: StatusCodes.Status409Conflict, title: "Conflict"),
                ErrorType.Unauthorized => Results.Problem(detail: error.Message, statusCode: StatusCodes.Status401Unauthorized, title: "Unauthorized"),
                ErrorType.Forbidden => Results.Problem(detail: error.Message, statusCode: StatusCodes.Status403Forbidden, title: "Forbidden"),
                _ => Results.Problem(detail: "An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError, title: "Server Error")
            };
        }
    }
}
