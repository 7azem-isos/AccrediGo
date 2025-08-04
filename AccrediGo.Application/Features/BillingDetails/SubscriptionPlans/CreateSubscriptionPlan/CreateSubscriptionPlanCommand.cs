using AccrediGo.Application.Interfaces;

namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommand : ICreateCommand<CreateSubscriptionPlanDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; }

        // Audit properties (implemented from IAuditableCommand)
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedFromIp { get; set; }
        public string UserAgent { get; set; }
        public string AuditContext { get; set; }

        public CreateSubscriptionPlanCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
} 