using FluentValidation;

namespace EvenDriven.Api.Middlewares
{
    public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var argument = context.Arguments.OfType<T>().First();

            var validationResult = await validator.ValidateAsync(
                argument,
                context.HttpContext.RequestAborted);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.Errors.
                    GroupBy(e => e.PropertyName).
                    ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()), statusCode: StatusCodes.Status400BadRequest);
            }

            return await next(context);
        }
    }
}
