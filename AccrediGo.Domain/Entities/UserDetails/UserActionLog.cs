using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.UserDetails
{
    /// <summary>
    /// Represents a log entry for a user's actions in the system.
    /// </summary>
    [Table("UserActionLogs")]
    public class UserActionLog : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the action log entry.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the user who performed the action.
        /// </summary>
        [Required]
        public string UserID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related user.
        /// </summary>
        [ForeignKey("UserID")]
        public User User { get; set; } = null!;

        /// <summary>
        /// The action performed by the user.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Action { get; set; } = null!;

        /// <summary>
        /// The context or details of the action.
        /// </summary>
        [MaxLength(1000)]
        public string? Context { get; set; }

        /// <summary>
        /// The timestamp when the action was performed.
        /// </summary>
        [Required]
        public DateTime TimeStamp { get; set; }
    }

}
