using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Enums;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.BillingDetails
{
    /// <summary>
    /// Represents a subscription for a facility in the system.
    /// </summary>
    [Table("Subscriptions")]
    public class Subscription : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the subscription.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the facility associated with this subscription.
        /// </summary>
        [Required]
        public string FacilityID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related facility.
        /// </summary>
        [ForeignKey("FacilityID")]
        public Facility Facility { get; set; } = null!;

        /// <summary>
        /// The identifier of the subscription plan.
        /// </summary>
        [Required]
        public string PlanID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related subscription plan.
        /// </summary>
        [ForeignKey("PlanID")]
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        /// <summary>
        /// The start date and time of the subscription.
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// The expiry date and time of the subscription.
        /// </summary>
        [Required]
        public DateTime Expiry { get; set; }

        /// <summary>
        /// The status of the subscription.
        /// </summary>
        [Required]
        public SubscriptionStatus Status { get; set; }

        /// <summary>
        /// The amount charged for the subscription.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Collection of payments associated with this subscription.
        /// </summary>
        public List<Payment> Payments { get; set; } = new();
    }


}
