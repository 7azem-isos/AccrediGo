using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.MainComponents
{
    /// <summary>
    /// Represents a standard within the accreditation system.
    /// </summary>
    [Table("Standards")]
    public class Standard : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the standard.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the chapter-accreditation-facility type relationship.
        /// </summary>
        [Required]
        public string ChapterAccreditationFacilityTypeId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related chapter-accreditation-facility type relationship.
        /// </summary>
        [ForeignKey("ChapterAccreditationFacilityTypeId")]
        public ChapterAccreditationFacilityType ChapterAccreditationFacilityType { get; set; } = null!;

        /// <summary>
        /// The code of the standard.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = null!;

        /// <summary>
        /// The description of the standard in English.
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// The description of the standard in Arabic.
        /// </summary>
        [MaxLength(1000)]
        public string? ArabicDescription { get; set; }

        /// <summary>
        /// The weight/importance of this standard.
        /// </summary>
        [Required]
        public int Weight { get; set; }

        /// <summary>
        /// Indicates whether this standard is applicable.
        /// </summary>
        [Required]
        public bool IsApplicable { get; set; }

        /// <summary>
        /// Collection of EoCs associated with this standard.
        /// </summary>
        public List<EoC> EoCs { get; set; } = new();
    }

}
