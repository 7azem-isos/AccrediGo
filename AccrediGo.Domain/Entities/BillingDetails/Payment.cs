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
    /// Represents a payment made for a subscription in the system.
    /// </summary>
    [Table("Payments")]
    public class Payment : BaseEntity
    {
        /// <summary>
        /// The unique identifier for the payment.
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The identifier of the facility associated with this payment.
        /// </summary>
        [Required]
        public string FacilityID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related facility.
        /// </summary>
        [ForeignKey("FacilityID")]
        public Facility Facility { get; set; } = null!;

        /// <summary>
        /// The identifier of the subscription associated with this payment.
        /// </summary>
        [Required]
        public string SubscriptionID { get; set; } = null!;

        /// <summary>
        /// Navigation property for the related subscription.
        /// </summary>
        [ForeignKey("SubscriptionID")]
        public Subscription Subscription { get; set; } = null!;

        /// <summary>
        /// The amount of the payment.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// The payment method (e.g., CreditCard, BankTransfer).
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Method { get; set; } = null!;

        /// <summary>
        /// The status of the payment.
        /// </summary>
        [Required]
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// The date and time when the payment was made.
        /// </summary>
        [Required]
        public DateTime PaidAt { get; set; }

        /// <summary>
        /// The currency of the payment (e.g., USD, SAR).
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string Currency { get; set; } = null!;
    }

}
