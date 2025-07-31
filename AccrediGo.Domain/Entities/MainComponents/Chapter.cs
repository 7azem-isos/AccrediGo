using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.MainComponents
{
    public class Chapter : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the chapter.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The title of the chapter in English.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// The title of the chapter in Arabic.
        /// </summary>
        [MaxLength(200)]
        public string? ArabicTitle { get; set; }

        /// <summary>
        /// The weight/importance of this chapter.
        /// </summary>
        [Required]
        public int Weight { get; set; }

        /// <summary>
        /// Collection of chapter-accreditation-facility type relationships.
        /// </summary>
        public List<ChapterAccreditationFacilityType> ChapterAccreditationFacilityTypes { get; set; } = new();
    }

}
