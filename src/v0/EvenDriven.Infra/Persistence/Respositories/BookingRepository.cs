using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Enums;
using EvenDriven.Domain.Primitives;
using EvenDriven.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace EvenDriven.Infra.Persistence.Respositories
{
    public sealed class BookingRepository : BaseRepository<BookingEntity>, IBookingRepository
    {
        public BookingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Result<BookingEntity>> GetBookingByIdAsync(Guid bookingId, Guid userId, CancellationToken cancellationToken = default)
        {
            var booking = await _dbContext.Booking.FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId, cancellationToken);
            if(booking == null)
            {
                return Result<BookingEntity>.Failure(new Error("404", $"Booking with ID {bookingId} not found.", ErrorType.NotFound));
            }
            return Result<BookingEntity>.Success(booking);
        }

        public async Task<Result<PagedResult<BookingEntity>>> GetBookingsByStatusAsync(string status, Guid userId, PagedQuery pagedQuery, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
            {
                return Result<PagedResult<BookingEntity>>.Failure( new Error("400", "Invalid booking status.", ErrorType.Validation));
            }

            
            var Data = _dbContext.Booking.Where(b => b.Status == parsedStatus && b.UserId == userId);
            var totalCount = await Data.CountAsync(cancellationToken);
            return Result<PagedResult<BookingEntity>>.Success(new PagedResult<BookingEntity>()
            {
                Items = await Data.Skip(pagedQuery.skip).Take(pagedQuery.PageSize).ToListAsync(cancellationToken),
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalCount = totalCount
            });
        }

        public async Task<Result<PagedResult<BookingEntity>>> GetBookingsByUserIdAsync(Guid userId, PagedQuery pagedQuery, CancellationToken cancellationToken = default)
        {
            var Data = _dbContext.Booking.Where(b => b.UserId == userId);
            var totalCount = await Data.CountAsync(cancellationToken);
            return Result<PagedResult<BookingEntity>>.Success(new PagedResult<BookingEntity>()
            {
                Items = await Data.Skip(pagedQuery.skip).Take(pagedQuery.PageSize).ToListAsync(cancellationToken),
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalCount = totalCount
            });
        }

        public async Task<Result<BookingEntity>> UpdateStatusAsync(Guid bookingId, string newStatus, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<BookingStatus>(newStatus, true, out var parsedNewStatus))
            {
                return Result<BookingEntity>.Failure(new Error("400", "Invalid booking status.", ErrorType.Validation));
            }

            var booking = await _dbContext.Booking.FindAsync(bookingId, cancellationToken);

            if (booking == null)
            {
                return Result<BookingEntity>.Failure(new Error("404", $"Booking with ID {bookingId} not found.", ErrorType.NotFound));
            }

            booking.Status = parsedNewStatus;

            _dbContext.Booking.Update(booking);

            return Result<BookingEntity>.Success(booking);
        }

    }
}
