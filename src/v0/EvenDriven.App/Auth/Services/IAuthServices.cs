using EvenDriven.App.Auth.DTOs;
using EvenDriven.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Auth.Services
{
    public interface IAuthServices
    {
        Task<Result<UserAuthResDto>> AuthenticateUserAsync(UserAuthReqDto request, CancellationToken cancellationToken = default);
    }
}
