using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.Domain.Entities.BaseModels
{


    public abstract class BaseEntity : IAuditableCommand
    {
        /// <summary>
        /// The user agent string of the client that created the entity.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// The IP address from which the entity was created.
        /// </summary>
        public string? CreatedFromIp { get; set; }

        /// <summary>
        /// Audit context for tracking additional info.
        /// </summary>
        public string AuditContext { get; set; } = string.Empty;
        /// <summary>
        /// The date and time when the entity was created.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
