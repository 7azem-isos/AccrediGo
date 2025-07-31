using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.SessionDetails
{
    /// <summary>
    /// Represents a component of a gap analysis session, capturing a user's answer to a question.
    /// </summary>
    [Table("SessionComponents")]
    public class SessionComponent : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the session component.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the session this component belongs to.
        /// </summary>
        [Required]
        public string SessionId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related session.
        /// </summary>
        [ForeignKey("SessionId")]
        public GapAnalysisSession GapAnalysisSession { get; set; } = null!;

        /// <summary>
        /// The identifier of the question this component is answering.
        /// </summary>
        [Required]
        public string QuestionId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related question.
        /// </summary>
        [ForeignKey("QuestionId")]
        public Question Question { get; set; } = null!;

        /// <summary>
        /// The user's answer to the question.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Answer { get; set; } = null!;

        /// <summary>
        /// The status of the user's answer (e.g., Met, PartiallyMet, NotMet).
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string AnswerStatus { get; set; } = null!;
    }

}
