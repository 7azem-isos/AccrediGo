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
    public class ChapterAccreditationFacilityType : BaseEntity
    {
        /// <summary>
        /// The unique identifier for this relationship.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the chapter.
        /// </summary>
        [Required]
        public string ChapterId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related chapter.
        /// </summary>
        [ForeignKey("ChapterId")]
        public Chapter Chapter { get; set; } = null!;

        /// <summary>
        /// The identifier of the accreditation.
        /// </summary>
        [Required]
        public string AccreditationId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related accreditation.
        /// </summary>
        [ForeignKey("AccreditationId")]
        public Accreditation Accreditation { get; set; } = null!;

        /// <summary>
        /// The identifier of the facility type.
        /// </summary>
        [Required]
        public int FacilityTypeId { get; set; }

        /// <summary>
        /// Navigation property for the related facility type.
        /// </summary>
        [ForeignKey("FacilityTypeId")]
        public FacilityType FacilityType { get; set; } = null!;

        /// <summary>
        /// Additional notes for this relationship.
        /// </summary>
        [MaxLength(1000)]
        public string? Notes { get; set; }

        /// <summary>
        /// Collection of standards associated with this relationship.
        /// </summary>
        public List<Standard> Standards { get; set; } = new();
    }

}
