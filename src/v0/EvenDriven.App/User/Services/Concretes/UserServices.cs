using EvenDriven.App.Common;
using EvenDriven.App.Common.Services;
using EvenDriven.App.User.DTOs;
using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Primitives;
using Microsoft.Extensions.Logging;


namespace EvenDriven.App.User.Services.Concretes
{
    public sealed class UserServices : IUserServices
    {
        private readonly ILogger<UserServices> _logger;
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IPasswordServices _passwordServices;

        public UserServices(ILogger<UserServices> logger, IUnitOfWork unitOfWork, IBaseRepository<UserEntity> userRepository, IPasswordServices passwordServices)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _passwordServices = passwordServices;
        }

        public async Task<Result<UserDtoRes>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching user by ID: {UserId}", userId);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new InvalidOperationException("User not found");
            if(user.IsSuccess == false)
            {
                _logger.LogWarning("User not found with ID: {UserId}", userId);
                return Result<UserDtoRes>.Failure(user.Error);
            }
            _logger.LogInformation("User fetched successfully with ID: {UserId}", userId);
            return Result<UserDtoRes>.Success(UserDtoRes.FromEntity(user.Value));
        }

        public async Task<Result<UserDtoRes>> RegisterUserAsync(RegisterUserDtoReq registerUserDtoReq, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Registering new user with email: {Email}", registerUserDtoReq.Email);
            UserEntity User = UserEntity.Create(
                    registerUserDtoReq.FirstName,
                    registerUserDtoReq.LastName,
                    registerUserDtoReq.Email,
                    _passwordServices.HashPassword(registerUserDtoReq.Password));

            await _userRepository.AddAsync(User, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("User registered successfully with email: {Email}", registerUserDtoReq.Email);
            return Result<UserDtoRes>.Success(UserDtoRes.FromEntity(User));
        }
    }
}
