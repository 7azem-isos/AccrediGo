using System;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.Application.Interfaces
{
    /// <summary>
    /// Service for automatically populating audit information
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// Populates audit information for a command
        /// </summary>
        /// <param name="command">The command to populate audit info for</param>
        void PopulateAuditInfo(IAuditableCommand command);

        /// <summary>
        /// Gets the current user ID
        /// </summary>
        string GetCurrentUserId();

        /// <summary>
        /// Gets the current user's IP address
        /// </summary>
        string GetCurrentUserIp();

        /// <summary>
        /// Gets the current user's user agent
        /// </summary>
        string GetCurrentUserAgent();

        /// <summary>
        /// Gets the current timestamp
        /// </summary>
        DateTime GetCurrentTimestamp();
    }
} 