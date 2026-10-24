using EvenDriven.App.Common.Services;

namespace EvenDriven.Api.Middlewares
{
    public sealed class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;
        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync( HttpContext httpContext, IJwtServices jwtServices)
        {
            var userId  = jwtServices.GetUserIdFromToken(httpContext.User);
            httpContext.Items["CurrentUserId"] = userId;

            await _next(httpContext);
        }
    }
}
