using EvenDriven.Api.Services;
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

        public async Task InvokeAsync( HttpContext httpContext, IJwtServices jwtServices, IUserContextAccessor userContextAccessor)
        {
            var userId  = jwtServices.GetUserIdFromToken(httpContext.User);

            userContextAccessor.SetUserId(userId);

            await _next(httpContext);
        }
    }
}
