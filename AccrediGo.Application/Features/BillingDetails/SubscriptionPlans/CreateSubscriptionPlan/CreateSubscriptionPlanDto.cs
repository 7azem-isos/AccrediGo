namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanDto
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Pricing { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
} 