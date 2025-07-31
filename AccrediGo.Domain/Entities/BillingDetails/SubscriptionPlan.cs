using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BillingDetails;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities
{
    /// <summary>
    /// Represents a subscription plan available for facilities in the system.
    /// </summary>
    [Table("SubscriptionPlans")]
    public class SubscriptionPlan : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the subscription plan.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The type or name of the subscription plan (e.g., Basic, Premium).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Type { get; set; } = null!;

        /// <summary>
        /// The pricing of the subscription plan.
        /// </summary>
        [Required]
        public int Pricing { get; set; }

        /// <summary>
        /// Collection of subscriptions associated with this plan.
        /// </summary>
        public List<Subscription> Subscriptions { get; set; } = new();

        /// <summary>
        /// Collection of feature mappings associated with this subscription plan.
        /// </summary>
        public List<SubscriptionPlanFeature> SubscriptionPlanFeatures { get; set; } = new();
    }

}
