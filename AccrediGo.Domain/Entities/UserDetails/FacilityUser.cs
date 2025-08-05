using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.Roles;
using AccrediGo.Domain.Entities.BaseModels;
using AccrediGo.Domain.Entities.SessionDetails;

namespace AccrediGo.Domain.Entities.UserDetails
{
    /// <summary>
    /// Represents a user's association with a facility and their role within it.
    /// </summary>
    [Table("FacilityUsers")]
    public class FacilityUser : BaseEntity
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
        /// The identifier of the facility this user is associated with (Facility.UserId).
        /// <summary>
        /// The identifier of the facility this user is associated with (Facility.UserId).
        /// </summary>
        [Required]
        public string FacilityUserId { get; set; } = null!;
        
        /// <summary>
        /// Navigation property for the related facility.
        /// </summary>
        [ForeignKey("FacilityUserId")]
        public Facility Facility { get; set; } = null!;
        /// The identifier of the role this user has within the facility.
        /// </summary>
        [Required]
        public int FacilityRoleID { get; set; }

        /// <summary>
        /// Navigation property for the related facility role.
        /// </summary>
        [ForeignKey("FacilityRoleID")]
        public FacilityRole FacilityRole { get; set; } = null!;

        public List<ActionPlanComponent> ActionPlanComponents { get; set; } = new();
    }

}
