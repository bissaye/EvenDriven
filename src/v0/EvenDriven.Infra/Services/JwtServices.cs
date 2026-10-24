using Bissaye.JwtAuth.Services;
using EvenDriven.App.Common.Services;
using EvenDriven.App.User.DTOs;
using EvenDriven.Domain.Entities;
using System.Security.Claims;
using System.Security.Principal;


namespace EvenDriven.Infra.Services
{
    public sealed class JwtServices : IJwtServices
    {

        private readonly ITokenServices _tokenServices;

        public JwtServices(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }

        public string GenerateToken(UserClaim userClaim)
        {
            var claims = _tokenServices.GetClaims(userClaim); 
            return _tokenServices.GenerateToken(claims, false);
        }

        public Guid? GetUserIdFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return _tokenServices.GetClaimsValue<UserClaim?>(claimsPrincipal)?.id;
        }
    }
}
