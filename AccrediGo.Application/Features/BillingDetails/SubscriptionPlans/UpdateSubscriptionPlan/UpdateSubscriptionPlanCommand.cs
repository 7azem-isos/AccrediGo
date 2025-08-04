using AccrediGo.Domain.Entities.BillingDetails;
using MediatR;
using AutoMapper;
using AccrediGo.Application.Interfaces;

namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.UpdateSubscriptionPlan
{
    public class UpdateSubscriptionPlanCommand : IRequest<UpdateSubscriptionPlanDto>, IMapTo<SubscriptionPlan>
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Pricing { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSubscriptionPlanCommand, SubscriptionPlan>();
        }
    }
} 