using EvenDriven.Api.Extensions;
using EvenDriven.Api.Middlewares;
using EvenDriven.Api.Services;
using EvenDriven.App.User.DTOs;
using EvenDriven.App.User.Services;
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
            .AddEndpointFilter<ValidationFilter<RegisterUserDtoReq>>()
            .WithSummary("Register a new user")
            .AllowAnonymous();

            group.MapGet("/me", async (IUserServices userServices, IUserContext userContext, CancellationToken cancellationToken) =>
            {
                return (await userServices.GetUserByIdAsync((Guid)userContext.UserId, cancellationToken)).ToHttpResult();
            })
            .WithSummary("Get current user");
        }
    }
}
