using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Enums;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.SessionDetails
{
    /// <summary>
    /// Represents a component of an action plan linked to a session and improvement scenario.
    /// </summary>
    [Table("ActionPlanComponents")]
    public class ActionPlanComponent : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the action plan component.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the session this action plan component belongs to.
        /// </summary>
        [Required]
        public string SessionId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related session.
        /// </summary>
        [ForeignKey("SessionId")]
        public GapAnalysisSession GapAnalysisSession { get; set; } = null!;

        /// <summary>
        /// The identifier of the improvement scenario this action plan addresses.
        /// </summary>
        [Required]
        public string ScenarioId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related improvement scenario.
        /// </summary>
        [ForeignKey("ScenarioId")]
        public ImprovementScenario ImprovementScenario { get; set; } = null!;

        /// <summary>
        /// The identifier of the facility user assigned to this action plan component.
        /// </summary>
        [Required]
        public string AssignedTo { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related facility user.
        /// </summary>
        [ForeignKey("AssignedTo")]
        public FacilityUser FacilityUser { get; set; } = null!;

        /// <summary>
        /// The status of the action plan component's progress.
        /// </summary>
        [Required]
        public ProgressStatus ProgressStatus { get; set; }
    }

}
