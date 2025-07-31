using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Enums;
using AccrediGo.Domain.Entities.SessionDetails;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.MainComponents
{
    /// <summary>
    /// Represents a question used to evaluate an Element of Compliance (EoC).
    /// </summary>
    [Table("Questions")]
    public class Question : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the question.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the EoC this question evaluates.
        /// </summary>
        [Required]
        public string EoCId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related EoC.
        /// </summary>
        [ForeignKey("EoCId")]
        public EoC EoC { get; set; } = null!;

        /// <summary>
        /// The type of the question.
        /// </summary>
        [Required]
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// The text of the question in English.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; } = null!;

        /// <summary>
        /// The text of the question in Arabic.
        /// </summary>
        [MaxLength(1000)]
        public string? ArabicText { get; set; }

        /// <summary>
        /// Indicates whether the question is required to be answered.
        /// </summary>
        [Required]
        public bool IsRequired { get; set; }

        /// <summary>
        /// The identifier of the question this question depends on (if any).
        /// </summary>
        public string? DependsOnQuestionId { get; set; }

        /// <summary>
        /// Navigation property for the related dependent question.
        /// </summary>
        [ForeignKey("DependsOnQuestionId")]
        public Question? DependsOnQuestion { get; set; }

        /// <summary>
        /// Collection of session components associated with this question.
        /// </summary>
        public List<SessionComponent> SessionComponents { get; set; } = new();

        /// <summary>
        /// Collection of answer options associated with this question.
        /// </summary>
        public List<AnswerOption> AnswerOptions { get; set; } = new();
    }


}
