using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Enums;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.MainComponents
{
    /// <summary>
    /// Represents an Element of Compliance (EoC) in the accreditation standard.
    /// </summary>
    [Table("EoCs")]
    public class EoC : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the EoC.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The text of the EoC in English.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; } = null!;

        /// <summary>
        /// The text of the EoC in Arabic.
        /// </summary>
        [MaxLength(1000)]
        public string? ArabicText { get; set; }

        /// <summary>
        /// The compliance status of this EoC.
        /// </summary>
        [Required]
        public ComplianceStatus Status { get; set; }

        /// <summary>
        /// The identifier of the standard this EoC belongs to.
        /// </summary>
        [Required]
        public string StandardId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related standard.
        /// </summary>
        [ForeignKey("StandardId")]
        public Standard Standard { get; set; } = null!;

        /// <summary>
        /// Indicates whether this EoC is applicable.
        /// </summary>
        [Required]
        public bool IsApplicable { get; set; }

        /// <summary>
        /// Collection of questions associated with this EoC.
        /// </summary>
        public List<Question> Questions { get; set; } = new();
    }

}
