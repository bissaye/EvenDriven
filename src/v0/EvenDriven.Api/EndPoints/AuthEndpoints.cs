using EvenDriven.Api.Extensions;
using EvenDriven.App.Auth.DTOs;
using EvenDriven.App.Auth.Services;

namespace EvenDriven.Api.EndPoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/v0/auth")
                .WithTags("Auth");

            group.MapPost("/login", async (UserAuthReqDto request, IAuthServices authServices, CancellationToken cancellationToken) =>
            {
                return (await authServices.AuthenticateUserAsync(request, cancellationToken)).ToHttpResult();
            })
            .WithSummary("Authenticate a user and return a token")
            .AllowAnonymous();
        }
    }
}
