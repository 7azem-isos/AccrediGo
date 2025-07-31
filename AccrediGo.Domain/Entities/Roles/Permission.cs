using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.Roles
{
    /// <summary>
    /// Represents a permission in the system that can be assigned to system roles or facility roles.
    /// </summary>
    [Table("Permissions")]
    public class Permission : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the permission.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The code representing the permission (e.g., "ViewReports").
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Code { get; set; } = null!;

        /// <summary>
        /// The description of the permission.
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Collection of system role permissions associated with this permission.
        /// </summary>
        public List<SystemRolePermission> SystemRolePermissions { get; set; } = new();

        /// <summary>
        /// Collection of facility role permissions associated with this permission.
        /// </summary>
        public List<FacilityRolePermission> FacilityRolePermissions { get; set; } = new();
    }

}
