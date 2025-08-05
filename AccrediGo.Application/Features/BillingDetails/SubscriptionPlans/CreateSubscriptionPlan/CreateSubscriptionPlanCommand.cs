using AccrediGo.Application.Interfaces;
using AutoMapper;

namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommand : ICreateCommand<CreateSubscriptionPlanDto>, IMapTo<AccrediGo.Domain.Entities.BillingDetails.SubscriptionPlan>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSubscriptionPlanCommand, AccrediGo.Domain.Entities.BillingDetails.SubscriptionPlan>();
            profile.CreateMap<AccrediGo.Domain.Entities.BillingDetails.SubscriptionPlan, CreateSubscriptionPlanDto>();
        }
    }
} 