using EvenDriven.App.User.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EvenDriven.App.Common.Services
{
    public interface IJwtServices
    {
        string GenerateToken(UserClaim userClaim);
        Guid? GetUserIdFromToken(ClaimsPrincipal claimsPrincipal);
    }
}
