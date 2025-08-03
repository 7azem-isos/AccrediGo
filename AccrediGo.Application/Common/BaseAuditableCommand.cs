using System;
using AccrediGo.Application.Interfaces;

namespace AccrediGo.Application.Common
{
    /// <summary>
    /// Base class for auditable commands to reduce code duplication
    /// </summary>
    public abstract class BaseAuditableCommand : IAuditableCommand
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedFromIp { get; set; }
        public string UserAgent { get; set; }
        public string AuditContext { get; set; }

        protected BaseAuditableCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
} 