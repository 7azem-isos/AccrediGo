using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.Roles
{
    /// <summary>
    /// Represents a role that a user can have within a facility.
    /// </summary>
    [Table("FacilityRoles")]
    public class FacilityRole : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the facility role.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the facility role.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Collection of facility users associated with this role.
        /// </summary>
        public List<FacilityUser> FacilityUsers { get; set; } = new();

        /// <summary>
        /// Collection of permissions associated with this role.
        /// </summary>
        public List<FacilityRolePermission> FacilityRolePermissions { get; set; } = new();
    }

}
