using System;

namespace AccrediGo.Application.Interfaces
{
    /// <summary>
    /// Interface for commands that require audit tracking
    /// </summary>
    public interface IAuditableCommand
    {
        /// <summary>
        /// The ID of the user who performed the action
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The timestamp when the action was performed
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// The IP address of the user who performed the action
        /// </summary>
        // string CreatedFromIp { get; set; }

        /// <summary>
        /// The user agent/browser information
        /// </summary>
        // string UserAgent { get; set; }

        /// <summary>
        /// Additional context information for the audit
        /// </summary>
        string AuditContext { get; set; }
    }
} 