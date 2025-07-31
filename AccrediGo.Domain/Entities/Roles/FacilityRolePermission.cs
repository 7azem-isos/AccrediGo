using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.Roles
{
    /// <summary>
    /// Represents a mapping between a facility role and a permission in the system.
    /// </summary>
    [Table("FacilityRolePermissions")]
    public class FacilityRolePermission : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the facility role permission mapping.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the facility role.
        /// </summary>
        [Required]
        public int FacilityRoleID { get; set; }

        /// <summary>
        /// Navigation property for the related facility role.
        /// </summary>
        [ForeignKey("FacilityRoleID")]
        public FacilityRole FacilityRole { get; set; } = null!;

        /// <summary>
        /// The identifier of the permission.
        /// </summary>
        [Required]
        public string PermissionID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related permission.
        /// </summary>
        [ForeignKey("PermissionID")]
        public Permission Permission { get; set; } = null!;
    }

}
