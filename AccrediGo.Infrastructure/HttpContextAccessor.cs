using Microsoft.AspNetCore.Http;
using AccrediGo.Application.Services;
using System.Linq;

namespace AccrediGo.Infrastructure
{
    public class HttpContextAccessor : IHttpContextInfo
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

        public HttpContextAccessor(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserIpAddress()
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null) return null;

            // Try to get the real IP address (handles proxies)
            var forwardedHeader = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedHeader))
            {
                return forwardedHeader.Split(',')[0].Trim();
            }

            var remoteIp = httpContext.Connection?.RemoteIpAddress?.ToString();
            return remoteIp;
        }

        public string? GetUserAgent()
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null) return null;

            return httpContext.Request.Headers["User-Agent"].FirstOrDefault();
        }
    }
} 