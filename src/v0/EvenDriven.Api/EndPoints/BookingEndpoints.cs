using EvenDriven.Api.Extensions;
using EvenDriven.Api.Middlewares;
using EvenDriven.Api.Services;
using EvenDriven.App.Booking.DTOs;
using EvenDriven.App.Booking.Services;
using EvenDriven.App.Common.Services;
using EvenDriven.App.Common.Validators;
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

            group.MapPost("/book", async (BookingDtoReq contract, IBookingServices bookingServices, IUserContext userContext, CancellationToken cancellationToken) =>
            {
                return (await bookingServices.CreateBookingAsync(contract, userContext.UserId, cancellationToken)).ToHttpResult();
            })
            .AddEndpointFilter<ValidationFilter<BookingDtoReq>>()
            .WithSummary("Book a new slot");

            group.MapGet("/{bookingId:guid}", async (Guid bookingId, IBookingServices bookingServices, IUserContext userContext,CancellationToken cancellationToken) =>
            {
                return (await bookingServices.GetBookingByIdAsync(bookingId, userContext.UserId, cancellationToken)).ToHttpResult();
            }).WithSummary("Get a booking by ID");


            group.MapPut("/update-status/{bookingId:guid}", async (Guid bookingId, string newStatus, IBookingServices bookingServices, IUserContext userContext, CancellationToken cancellationToken) =>
            {
                return (await bookingServices.UpdateStatusAsync(bookingId, userContext.UserId, newStatus, cancellationToken)).ToHttpResult();
            }).WithSummary("Update booking status");


            group.MapGet("/my-bookings", async (PagedQueryDto pagedQueryDto, IBookingServices bookingServices, IUserContext userContext, CancellationToken cancellationToken) =>
            {
                return (await bookingServices.GetBookingsByUserIdAsync(userContext.UserId, pagedQueryDto.ToPagedQuery(), cancellationToken)).ToHttpResult();
            }).AddEndpointFilter<ValidationFilter<PagedQueryDto>>()
              .WithSummary("Get my bookings");


            group.MapGet("/my-bookings/status/{status}", async (string status, PagedQueryDto pagedQueryDto, IBookingServices bookingServices, IUserContext userContext, CancellationToken cancellationToken) =>
            {
                return (await bookingServices.GetBookingsByUserIdByStatusAsync(userContext.UserId, status, pagedQueryDto.ToPagedQuery() , cancellationToken)).ToHttpResult();
            }).AddEndpointFilter<ValidationFilter<PagedQueryDto>>()
              .WithSummary("Get my bookings");

        }
    }
}
