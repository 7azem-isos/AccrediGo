using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;
using AccrediGo.Domain.Entities.BillingDetails;
using AccrediGo.Domain.Entities.SessionDetails;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Enums;

namespace AccrediGo.Domain.Entities.MainComponents
{
    /// <summary>
    /// Represents a facility in the accreditation system.
    /// </summary>
    [Table("Facilities")]
    public class Facility : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the facility, which is also the UserId.
        /// </summary>
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related user.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The name of the facility in English.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The name of the facility in Arabic.
        /// </summary>
        [MaxLength(200)]
        public string? ArabicName { get; set; }

        /// <summary>
        /// The location of the facility in English.
        /// </summary>
        [MaxLength(500)]
        public string? Location { get; set; }

        /// <summary>
        /// The location of the facility in Arabic.
        /// </summary>
        [MaxLength(500)]
        public string? ArabicLocation { get; set; }

        /// <summary>
        /// The size category of the company.
        /// </summary>
        [Required]
        public CompanySize CompanySize { get; set; }

        /// <summary>
        /// The email address of the facility.
        /// </summary>
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// The phone number of the facility.
        /// </summary>
        [MaxLength(20)]
        public string? Phone { get; set; }

        /// <summary>
        /// The telephone number of the facility.
        /// </summary>
        [MaxLength(20)]
        public string? Tel { get; set; }

        /// <summary>
        /// The identifier of the accreditation this facility is associated with.
        /// </summary>
        [Required]
        public string AccreditationId { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related accreditation.
        /// </summary>
        [ForeignKey("AccreditationId")]
        public Accreditation Accreditation { get; set; } = null!;

        /// <summary>
        /// The identifier of the facility type.
        /// </summary>
        [Required]
        public int FacilityTypeId { get; set; }

        /// <summary>
        /// Navigation property for the related facility type.
        /// </summary>
        [ForeignKey("FacilityTypeId")]
        public FacilityType FacilityType { get; set; } = null!;

        /// <summary>
        /// Collection of payments associated with this facility.
        /// </summary>
        public List<Payment> Payments { get; set; } = new();

        /// <summary>
        /// Collection of facility users associated with this facility.
        /// </summary>
        public List<FacilityUser> FacilityUsers { get; set; } = new();

        /// <summary>
        /// Collection of subscriptions associated with this facility.
        /// </summary>
        public List<Subscription> Subscriptions { get; set; } = new();

        /// <summary>
        /// Collection of gap analysis sessions associated with this facility.
        /// </summary>
        public List<GapAnalysisSession> GapAnalysisSessions { get; set; } = new();
    }
}

