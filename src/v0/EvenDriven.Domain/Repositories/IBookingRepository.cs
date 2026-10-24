using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Repositories
{
    public interface IBookingRepository : IBaseRepository<BookingEntity>
    {
        Task<Result<PagedResult<BookingEntity>>> GetBookingsByUserIdAsync(Guid userId, PagedQuery pagedQuery, CancellationToken cancellationToken = default);
        Task<Result<BookingEntity>> GetBookingByIdAsync(Guid bookingId, Guid userId, CancellationToken cancellationToken = default);
        Task<Result<PagedResult<BookingEntity>>> GetBookingsByStatusAsync(string status, Guid userId, PagedQuery pagedQuery, CancellationToken cancellationToken = default);
        Task<Result<BookingEntity>> UpdateStatusAsync(Guid bookingId, string newStatus, CancellationToken cancellationToken = default);
    }
}
