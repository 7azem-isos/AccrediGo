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
    /// Represents a user's trial access to explore the system.
    /// </summary>
    [Table("ExploreUserAccess")]
    public class ExploreUserAccess : BaseEntity
    {
        /// <summary>
        /// The identifier of the user, also serving as the primary key.
        /// </summary>
        [Key]
        [ForeignKey("User")]
        public string UserID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related user.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The start date and time of the trial period.
        /// </summary>
        [Required]
        public DateTime TrialStart { get; set; }

        /// <summary>
        /// The end date and time of the trial period.
        /// </summary>
        [Required]
        public DateTime TrialEnd { get; set; }
    }

}
