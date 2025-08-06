using System.Text.Json.Serialization;
using AccrediGo.Application.Interfaces;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public string CreatedFromIp { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
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