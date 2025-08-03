using System;
using AccrediGo.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.Application.Services
{
    /// <summary>
    /// Service for automatically populating audit information
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly ICurrentRequest _currentRequest;
        private readonly IHttpContextInfo _httpContextInfo;
        private readonly ILogger<AuditService> _logger;

        public AuditService(
            ICurrentRequest currentRequest,
            IHttpContextInfo httpContextInfo,
            ILogger<AuditService> logger)
        {
            _currentRequest = currentRequest ?? throw new ArgumentNullException(nameof(currentRequest));
            _httpContextInfo = httpContextInfo ?? throw new ArgumentNullException(nameof(httpContextInfo));
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
                return _httpContextInfo?.GetUserIpAddress() ?? "Unknown";
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
                return _httpContextInfo?.GetUserAgent() ?? "Unknown";
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