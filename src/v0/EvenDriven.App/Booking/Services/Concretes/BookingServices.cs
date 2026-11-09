using EvenDriven.App.Booking.DTOs;
using EvenDriven.App.Common;
using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Primitives;
using EvenDriven.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Contracts;


namespace EvenDriven.App.Booking.Services.Concretes
{
    public sealed class BookingServices : IBookingServices
    {
        private readonly ILogger<BookingServices> _logger;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookingServices(ILogger<BookingServices> logger, IBookingRepository bookingRepository, IBaseRepository<UserEntity> userRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<BookingDtoRes>> CreateBookingAsync(BookingDtoReq bookingDtoReq, Guid currentUserId, CancellationToken cancellationToken = default)
        {
            if (currentUserId != bookingDtoReq.userId)
            {
                _logger.LogWarning("Unauthorized booking attempt by user ID: {CurrentUserId} for user ID: {BookingUserId}", currentUserId, bookingDtoReq.userId);
                return Result<BookingDtoRes>.Failure(new Error("401", "Unauthorized booking for this user.", ErrorType.Forbidden));
            }

            var checkUser = await _userRepository.DoesItExistAsync(u => u.Id == bookingDtoReq.userId, cancellationToken);
            if (checkUser.IsSuccess && checkUser.Value == false)
            {
                _logger.LogWarning("User with ID: {UserId} does not exist.", bookingDtoReq.userId);
                return Result<BookingDtoRes>.Failure( new Error("404", "User not found.", ErrorType.NotFound));
            }

            _logger.LogInformation("Creating booking for user ID: {UserId} from {StartDate} to {EndDate}", bookingDtoReq.userId, bookingDtoReq.startDate, bookingDtoReq.endDate);
            BookingEntity bookingEntity = BookingEntity.Create(
                bookingDtoReq.userId, 
                bookingDtoReq.startDate, 
                bookingDtoReq.endDate
            );

            await _bookingRepository.AddAsync(bookingEntity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Booking created successfully with ID: {BookingId}", bookingEntity.Id);
            return Result<BookingDtoRes>.Success(BookingDtoRes.FromEntity(bookingEntity));
        }

        public async Task<Result<BookingDtoRes>> GetBookingByIdAsync(Guid bookingId, Guid userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching booking with ID: {BookingId} for user ID: {UserId}", bookingId, userId);
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId, userId, cancellationToken);

            if (!booking.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch booking with ID: {BookingId} for user ID: {UserId}. Error: {Error}", bookingId, userId, booking.Error);
                return Result<BookingDtoRes>.Failure(booking.Error);
            }

            _logger.LogInformation("Booking fetched successfully with ID: {BookingId} for user ID: {UserId}", bookingId, userId);
            return Result<BookingDtoRes>.Success(BookingDtoRes.FromEntity(booking.Value));
        }

        public async Task<Result<PagedResult<BookingDtoRes>>> GetBookingsByUserIdAsync(Guid userId, PagedQuery pagedQuery, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching bookings for user ID: {UserId} with pagination - Page: {Page}, PageSize: {PageSize}", userId, pagedQuery.Page, pagedQuery.PageSize);
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId, pagedQuery, cancellationToken);

            if (!bookings.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch bookings for user ID: {UserId}. Error: {Error}", userId, bookings.Error);
                return Result<PagedResult<BookingDtoRes>>.Failure(bookings.Error);
            }

            return Result<PagedResult<BookingDtoRes>>.Success(new PagedResult<BookingDtoRes>
            {
                Items = bookings.Value.Items.Select(BookingDtoRes.FromEntity).ToList(),
                Page = bookings.Value.Page,
                PageSize = bookings.Value.PageSize,
                TotalCount = bookings.Value.TotalCount
            });
        }

        public async Task<Result<PagedResult<BookingDtoRes>>> GetBookingsByUserIdByStatusAsync(Guid userId, string status, PagedQuery pagedQuery, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching bookings for user ID: {UserId} with status: {Status} and pagination - Page: {Page}, PageSize: {PageSize}", userId, status, pagedQuery.Page, pagedQuery.PageSize);
            var bookings = await _bookingRepository.GetBookingsByStatusAsync(status, userId, pagedQuery, cancellationToken);
            
            if(!bookings.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch bookings for user ID: {UserId} with status: {Status}. Error: {Error}", userId, status, bookings.Error);
                return Result<PagedResult<BookingDtoRes>>.Failure(bookings.Error);
            }

            return Result<PagedResult<BookingDtoRes>>.Success(new PagedResult<BookingDtoRes>
            {
                Items = bookings.Value.Items.Select(BookingDtoRes.FromEntity).ToList(),
                Page = bookings.Value.Page,
                PageSize = bookings.Value.PageSize,
                TotalCount = bookings.Value.TotalCount
            });
        }

        public async Task<Result<BookingDtoRes>> UpdateStatusAsync(Guid bookingId, Guid userId, string newStatus, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating status of booking ID: {BookingId} for user ID: {UserId} to new status: {NewStatus}", bookingId, userId, newStatus);
            var booking = await _bookingRepository.GetByConditionAsync(b => b.Id == bookingId && b.UserId == userId, cancellationToken);
            
            if (!booking.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch booking with ID: {BookingId} for user ID: {UserId}. Error: {Error}", bookingId, userId, booking.Error);
                return Result<BookingDtoRes>.Failure(booking.Error);
            }

            Result<BookingEntity> updateResult = await _bookingRepository.UpdateStatusAsync(bookingId, newStatus, cancellationToken);
            
            if(!updateResult.IsSuccess)
            {
                _logger.LogWarning("Failed to update status of booking ID: {BookingId} for user ID: {UserId}. Error: {Error}", bookingId, userId, updateResult.Error);
                return Result<BookingDtoRes>.Failure(updateResult.Error);
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Status of booking ID: {BookingId} for user ID: {UserId} updated successfully to new status: {NewStatus}", bookingId, userId, newStatus);
            return Result<BookingDtoRes>.Success(BookingDtoRes.FromEntity(updateResult.Value));
        }
    }
}
