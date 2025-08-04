namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.UpdateSubscriptionPlan
{
    public class UpdateSubscriptionPlanDto
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Pricing { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 