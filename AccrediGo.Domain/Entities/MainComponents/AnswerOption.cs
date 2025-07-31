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
    /// Represents an answer option for a question, with a one-to-one relationship to an improvement scenario.
    /// </summary>
    [Table("AnswerOptions")]
    public class AnswerOption : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the answer option.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the question this answer option belongs to.
        /// </summary>
        [Required]
        public string QuestionId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related question.
        /// </summary>
        [ForeignKey("QuestionId")]
        public Question Question { get; set; } = null!;

        /// <summary>
        /// The text of the answer option in English.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string OptionText { get; set; } = null!;

        /// <summary>
        /// The text of the answer option in Arabic.
        /// </summary>
        [MaxLength(500)]
        public string? ArabicOptionText { get; set; }

        /// <summary>
        /// The identifier of the improvement scenario associated with this answer option.
        /// </summary>
        [Required]
        public string ImprovementScenarioId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related improvement scenario.
        /// </summary>
        [ForeignKey("ImprovementScenarioId")]
        public ImprovementScenario ImprovementScenario { get; set; } = null!;
    }

}
