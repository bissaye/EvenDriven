using EvenDriven.App.User.DTOs;
using EvenDriven.Domain.Primitives;

namespace EvenDriven.App.User.Services
{
    public interface IUserServices
    {
        Task<Result<UserDtoRes>> RegisterUserAsync(RegisterUserDtoReq registerUserDtoReq, CancellationToken cancellationToken = default);
        Task<Result<UserDtoRes>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
