using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.SessionDetails;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.MainComponents
{
    /// <summary>
    /// Represents an improvement scenario associated with an answer option, with a one-to-one relationship.
    /// </summary>
    [Table("ImprovementScenarios")]
    public class ImprovementScenario : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the improvement scenario.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the answer option this scenario is associated with.
        /// </summary>
        [Required]
        public string AnswerOptionId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related answer option.
        /// </summary>
        public AnswerOption AnswerOption { get; set; } = null!;

        /// <summary>
        /// The text of the improvement scenario in English.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string ScenarioText { get; set; } = null!;

        /// <summary>
        /// The text of the improvement scenario in Arabic.
        /// </summary>
        [MaxLength(1000)]
        public string? ArabicScenarioText { get; set; }

        /// <summary>
        /// Collection of action plan components associated with this improvement scenario.
        /// </summary>
        public List<ActionPlanComponent> ActionPlanComponents { get; set; } = new();
    }

}
