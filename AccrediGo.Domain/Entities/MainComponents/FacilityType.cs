using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.MainComponents
{
    [Table("FacilityTypes")]
    public class FacilityType : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the facility type.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the facility type in English.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; } = null!;

        /// <summary>
        /// The name of the facility type in Arabic.
        /// </summary>
        [MaxLength(100)]
        public string? ArabicTypeName { get; set; }

        /// <summary>
        /// Collection of facilities of this type.
        /// </summary>
        public List<Facility> Facilities { get; set; } = new();

        /// <summary>
        /// Collection of chapter-accreditation-facility type relationships.
        /// </summary>
        public List<ChapterAccreditationFacilityType> ChapterAccreditationFacilityTypes { get; set; } = new();
    }

}
