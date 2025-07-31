using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;
using AccrediGo.Domain.Entities.SessionDetails;

namespace AccrediGo.Domain.Entities.MainComponents
{
    [Table("Accreditations")]
    public class Accreditation : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the accreditation.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The name of the accreditation in English.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The name of the accreditation in Arabic.
        /// </summary>
        [MaxLength(200)]
        public string? ArabicName { get; set; }

        /// <summary>
        /// The description of the accreditation in English.
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// The description of the accreditation in Arabic.
        /// </summary>
        [MaxLength(1000)]
        public string? ArabicDescription { get; set; }

        /// <summary>
        /// Collection of gap analysis sessions associated with this accreditation.
        /// </summary>
        public List<GapAnalysisSession> GapAnalysisSessions { get; set; } = new();

        /// <summary>
        /// Collection of chapter-accreditation-facility type relationships.
        /// </summary>
        public List<ChapterAccreditationFacilityType> ChapterAccreditationFacilityTypes { get; set; } = new();

        /// <summary>
        /// Collection of facilities associated with this accreditation.
        /// </summary>
        public List<Facility> Facilities { get; set; } = new();
    }

}
