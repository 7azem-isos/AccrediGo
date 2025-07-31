using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;

namespace AccrediGo.Domain.Entities.BillingDetails
{
    /// <summary>
    /// Represents a mapping between a subscription plan and a feature.
    /// </summary>
    [Table("SubscriptionPlanFeatures")]
    public class SubscriptionPlanFeature : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the subscription plan feature mapping.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the subscription plan.
        /// </summary>
        [Required]
        public string SubscriptionPlanID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related subscription plan.
        /// </summary>
        [ForeignKey("SubscriptionPlanID")]
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        /// <summary>
        /// The identifier of the feature.
        /// </summary>
        [Required]
        public string FeatureID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related feature.
        /// </summary>
        [ForeignKey("FeatureID")]
        public Feature Feature { get; set; } = null!;
    }

}
