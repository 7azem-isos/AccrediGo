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
    /// Represents a feature that can be associated with subscription plans.
    /// </summary>
    [Table("Features")]
    public class Feature : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the feature.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The description of the feature.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Text { get; set; } = null!;

        /// <summary>
        /// The description of the feature in Arabic.
        /// </summary>
        [MaxLength(500)]
        public string? ArabicText { get; set; }

        /// <summary>
        /// Collection of subscription plan mappings associated with this feature.
        /// </summary>
        public List<SubscriptionPlanFeature> SubscriptionPlanFeatures { get; set; } = new();
    }

}
