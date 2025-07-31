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
    /// Represents a gap analysis session for a facility.
    /// </summary>
    [Table("GapAnalysisSessions")]
    public class GapAnalysisSession : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the gap analysis session.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the facility this session is associated with.
        /// </summary>
        [Required]
        public string FacilityId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related facility.
        /// </summary>
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; } = null!;

        /// <summary>
        /// The start date and time of the session.
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// The end date and time of the session.
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// Collection of session components associated with this session.
        /// </summary>
        public List<SessionComponent> SessionComponents { get; set; } = new();

        /// <summary>
        /// Collection of action plan components associated with this session.
        /// </summary>
        public List<ActionPlanComponent> ActionPlanComponents { get; set; } = new();
    }

}
