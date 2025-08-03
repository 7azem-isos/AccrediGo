using System;
using AccrediGo.Application.Interfaces;
using AccrediGo.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AccrediGo.Application.Services
{
    /// <summary>
    /// Service for automatically populating audit information
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly ICurrentRequest _currentRequest;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuditService> _logger;

        public AuditService(
            ICurrentRequest currentRequest,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AuditService> logger)
        {
            _currentRequest = currentRequest ?? throw new ArgumentNullException(nameof(currentRequest));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void PopulateAuditInfo(IAuditableCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogWarning("Attempted to populate audit info for null command");
                    return;
                }

                command.CreatedBy = GetCurrentUserId();
                command.CreatedAt = GetCurrentTimestamp();
                command.CreatedFromIp = GetCurrentUserIp();
                command.UserAgent = GetCurrentUserAgent();
                command.AuditContext = $"User {command.CreatedBy} created from IP {command.CreatedFromIp}";

                _logger.LogDebug("Populated audit info for command: CreatedBy={CreatedBy}, CreatedAt={CreatedAt}, CreatedFromIp={CreatedFromIp}", 
                    command.CreatedBy, command.CreatedAt, command.CreatedFromIp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while populating audit info");
                // Don't throw - audit failure shouldn't break the main operation
            }
        }

        public string GetCurrentUserId()
        {
            return _currentRequest?.UserId ?? "Unknown";
        }

        public string GetCurrentUserIp()
        {
            try
            {
                var httpContext = _httpContextAccessor?.HttpContext;
                if (httpContext == null) return "Unknown";

                // Try to get the real IP address (handles proxies)
                var forwardedHeader = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (!string.IsNullOrEmpty(forwardedHeader))
                {
                    return forwardedHeader.Split(',')[0].Trim();
                }

                var remoteIp = httpContext.Connection?.RemoteIpAddress?.ToString();
                return remoteIp ?? "Unknown";
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error getting current user IP");
                return "Unknown";
            }
        }

        public string GetCurrentUserAgent()
        {
            try
            {
                var httpContext = _httpContextAccessor?.HttpContext;
                if (httpContext == null) return "Unknown";

                return httpContext.Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error getting current user agent");
                return "Unknown";
            }
        }

        public DateTime GetCurrentTimestamp()
        {
            return DateTime.UtcNow;
        }
    }
} 