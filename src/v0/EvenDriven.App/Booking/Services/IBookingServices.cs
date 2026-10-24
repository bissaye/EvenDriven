using EvenDriven.App.Booking.DTOs;
using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Booking.Services
{
    public interface IBookingServices
    {
        Task<Result<BookingDtoRes>> CreateBookingAsync(Guid userId, BookingDtoReq bookingDtoReq, CancellationToken cancellationToken = default);
        Task<Result<BookingDtoRes>> GetBookingByIdAsync(Guid bookingId, Guid userId, CancellationToken cancellationToken = default);
        Task<Result<PagedResult<BookingDtoRes>>> GetBookingsByUserIdAsync(Guid userId, PagedQuery pagedQuery, CancellationToken cancellationToken = default);
        Task<Result<PagedResult<BookingDtoRes>>> GetBookingsByUserIdByStatusAsync(Guid userId, string status, PagedQuery pagedQuery, CancellationToken cancellationToken = default);
        Task<Result<BookingDtoRes>> UpdateStatusAsync(Guid bookingId, Guid userId,string newStatus, CancellationToken cancellationToken = default);
    }
}
