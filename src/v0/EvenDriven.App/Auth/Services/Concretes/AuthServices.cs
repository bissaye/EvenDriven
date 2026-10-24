using EvenDriven.App.Auth.DTOs;
using EvenDriven.App.Common.Services;
using EvenDriven.App.User.DTOs;
using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Primitives;
using Microsoft.Extensions.Logging;

namespace EvenDriven.App.Auth.Services.Concretes
{
    public sealed class AuthServices : IAuthServices
    {
        private readonly ILogger<AuthServices> _logger;
        private readonly IPasswordServices _passwordServices;
        private readonly IJwtServices _jwtServices;
        private readonly IBaseRepository<UserEntity> _userRepository;
        public AuthServices(ILogger<AuthServices> logger, IPasswordServices passwordServices, IJwtServices jwtServices, IBaseRepository<UserEntity> userRepository)
        {
            _logger = logger;
            _passwordServices = passwordServices;
            _jwtServices = jwtServices;
            _userRepository = userRepository;
        }
        public async Task<Result<UserAuthResDto>> AuthenticateUserAsync(UserAuthReqDto request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Authenticating user with email: {Email}", request.email);
            var user = await _userRepository.GetByConditionAsync(u => u.Email == request.email, cancellationToken);
            
            if (!user.IsSuccess)
            {
                _logger.LogWarning("User not found with email: {Email}", request.email);
                return Result<UserAuthResDto>.Failure(user.Error);
            }

            if (!_passwordServices.VerifyPassword(request.password, user.Value.PasswordHash))
            {
                _logger.LogWarning("Invalid password for user with email: {Email}", request.email);
                return Result<UserAuthResDto>.Failure(new Error("401", "Invalid password.", ErrorType.Unauthorized));
            }

            _logger.LogInformation("User authenticated successfully with email: {Email}", request.email);
            return Result<UserAuthResDto>.Success(UserAuthResDto.FromEntity(user.Value, _jwtServices.GenerateToken(UserClaim.FromEntity(user.Value))));
        }
    }
}
