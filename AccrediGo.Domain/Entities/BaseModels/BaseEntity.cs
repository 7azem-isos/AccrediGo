using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccrediGo.Domain.Entities.BaseModels
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// The date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The user who created the entity.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// The date and time when the entity was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// The user who last updated the entity.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Indicates whether the entity is soft-deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

    }
}
