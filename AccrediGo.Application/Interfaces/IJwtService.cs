using System.Security.Claims;
using AccrediGo.Domain.Entities.UserDetails;

namespace AccrediGo.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateToken(string token);
        DateTime GetExpirationTime();
    }
} 