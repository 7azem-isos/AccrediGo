namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.GetAllSubscriptionPlans
{
    public class GetAllSubscriptionPlansDto
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Pricing { get; set; }
    }
} 