using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.Roles;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.UserDetails
{
    /// <summary>
    /// Represents a user in the system, serving as a base for facility users and explore users.
    /// </summary>
    [Table("Users")]
    public class User : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The name of the user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The name of the user in Arabic.
        /// </summary>
        [MaxLength(100)]
        public string? ArabicName { get; set; }

        /// <summary>
        /// The unique email address of the user.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// The identifier of the system role assigned to the user.
        /// </summary>
        [Required]
        public int SystemRoleId { get; set; }

        /// <summary>
        /// Navigation property for the related system role.
        /// </summary>
        [ForeignKey("SystemRoleId")]
        public SystemRole SystemRole { get; set; } = null!;

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Navigation property for the related facility user, if any.
        /// </summary>
        public FacilityUser? FacilityUser { get; set; }

        /// <summary>
        /// Navigation property for the related explore user access, if any.
        /// </summary>
        public ExploreUserAccess? ExploreUserAccess { get; set; }

        /// <summary>
        /// Collection of action logs associated with the user.
        /// </summary>
        public List<UserActionLog> UserActionLogs { get; set; } = new();
    }

}
