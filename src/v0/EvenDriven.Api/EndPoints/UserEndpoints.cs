using EvenDriven.Api.Extensions;
using EvenDriven.App.Common.Services;
using EvenDriven.App.User.DTOs;
using EvenDriven.App.User.Services;
using EvenDriven.Infra.Services;
using System.Security.Claims;

namespace EvenDriven.Api.EndPoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/v0/user")
                .WithTags("User")
                .RequireAuthorization();

            group.MapPost("/register", async (RegisterUserDtoReq request, IUserServices userServices, CancellationToken cancellationToken) =>
            {
                return (await userServices.RegisterUserAsync(request, cancellationToken)).ToHttpResult();
            })
            .WithSummary("Register a new user")
            .AllowAnonymous();

            group.MapGet("/me", async (IUserServices userServices, IJwtServices jwtServices, ClaimsPrincipal User, CancellationToken cancellationToken) =>
            {
                var userId = jwtServices.GetUserIdFromToken(User);
                if (userId == null)
                {
                    return Results.Unauthorized();
                }

                return (await userServices.GetUserByIdAsync((Guid)userId, cancellationToken)).ToHttpResult();
            })
            .WithSummary("Get current user");
        }
    }
}
