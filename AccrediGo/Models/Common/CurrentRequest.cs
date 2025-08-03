using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AccrediGo.Models.Common
{
    public class CurrentRequest : ICurrentRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentRequest(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Lang => _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].FirstOrDefault()?.Split(',')[0] ?? "en";

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        public string UserEmail => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

        public int UserRoleId
        {
            get
            {
                var roleClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                return int.TryParse(roleClaim, out var roleId) ? roleId : 0;
            }
        }

        public string CorrelationId => _httpContextAccessor.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();

        public DateTime RequestTime => DateTime.UtcNow;
    }
} 