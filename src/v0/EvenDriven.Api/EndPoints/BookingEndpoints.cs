using EvenDriven.Api.Contracts.Booking;
using EvenDriven.Api.Extensions;
using EvenDriven.App.Booking.Services;
using EvenDriven.App.Common.Services;
using EvenDriven.Domain.Primitives;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvenDriven.Api.EndPoints
{
    public static class BookingEndpoints
    {
        public static void MapBookingEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/v0/booking")
                .WithTags("Bookings")
                .RequireAuthorization();

            group.MapPost("/book", async (BookingReqContract contract, IBookingServices bookingServices, IJwtServices jwtServices, ClaimsPrincipal User, CancellationToken cancellationToken = default) =>
            {
                var userId = jwtServices.GetUserIdFromToken(User);
                if (userId == null)
                {
                    return Results.Unauthorized();
                }

                return (await bookingServices.CreateBookingAsync(userId.Value, contract.ToBookingDto((Guid)userId), cancellationToken)).ToHttpResult();

            }).WithSummary("Book a new slot");

            group.MapGet("/{bookingId:guid}", async (Guid bookingId, IBookingServices bookingServices, IJwtServices jwtServices, ClaimsPrincipal User, CancellationToken cancellationToken = default) =>
            {
                var userId = jwtServices.GetUserIdFromToken(User);
                if (userId == null)
                {
                    return Results.Unauthorized();
                }
                return (await bookingServices.GetBookingByIdAsync(bookingId, userId.Value, cancellationToken)).ToHttpResult();
            }).WithSummary("Get a booking by ID");


            group.MapPut("/update-status/{bookingId:guid}/{newStatus}", async (Guid bookingId, string newStatus, IBookingServices bookingServices, IJwtServices jwtServices, ClaimsPrincipal User, CancellationToken cancellationToken = default) =>
            {
                var userId = jwtServices.GetUserIdFromToken(User);
                if (userId == null)
                {
                    return Results.Unauthorized();
                }

                return (await bookingServices.UpdateStatusAsync(bookingId, userId.Value, newStatus, cancellationToken)).ToHttpResult();
            }).WithSummary("Update booking status");


            group.MapGet("/my-bookings", async ([FromQuery] int pageSize , [FromQuery] int pageNumber , IBookingServices bookingServices, IJwtServices jwtServices, ClaimsPrincipal User, CancellationToken cancellationToken = default) =>
            {
                var userId = jwtServices.GetUserIdFromToken(User);
                if (userId == null)
                {
                    return Results.Unauthorized();
                }


                return (await bookingServices.GetBookingsByUserIdAsync(userId.Value, new PagedQuery()
                {
                    Page = pageNumber,
                    PageSize = pageSize
                }, cancellationToken)).ToHttpResult();
            }).WithSummary("Get my bookings");


            group.MapGet("/my-bookings/status/{status}", async (string status, [FromQuery] int pageSize, [FromQuery] int pageNumber, IBookingServices bookingServices, IJwtServices jwtServices, ClaimsPrincipal User, CancellationToken cancellationToken = default) =>
            {
                var userId = jwtServices.GetUserIdFromToken(User);
                if (userId == null)
                {
                    return Results.Unauthorized();
                }


                return (await bookingServices.GetBookingsByUserIdByStatusAsync(userId.Value, status,  new PagedQuery()
                {
                    Page = pageNumber,
                    PageSize = pageSize
                }, cancellationToken)).ToHttpResult();
            }).WithSummary("Get my bookings");

        }
    }
}
