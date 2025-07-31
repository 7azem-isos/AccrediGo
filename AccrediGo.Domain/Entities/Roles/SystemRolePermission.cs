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
    /// Represents a mapping between a system role and a permission in the system.
    /// </summary>
    [Table("SystemRolePermissions")]
    public class SystemRolePermission : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the system role permission mapping.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the system role.
        /// </summary>
        [Required]
        public int SystemRoleID { get; set; }

        /// <summary>
        /// Navigation property for the related system role.
        /// </summary>
        [ForeignKey("SystemRoleID")]
        public SystemRole SystemRole { get; set; } = null!;

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
