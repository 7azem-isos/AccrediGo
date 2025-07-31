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
    /// Represents a system role that defines user privileges across the system.
    /// </summary>
    [Table("SystemRoles")]
    public class SystemRole : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the system role.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the system role.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Collection of users associated with this system role.
        /// </summary>
        public List<User> Users { get; set; } = new();

        /// <summary>
        /// Collection of permissions associated with this system role.
        /// </summary>
        public List<SystemRolePermission> SystemRolePermissions { get; set; } = new();
    }

}
