using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Application.Commands.BillingDetails.SubscriptionPlan.CreateSubscriptionPlan;
using AccrediGo.Application.Queries.BillingDetails.SubscriptionPlan.ReadAllSubscriptionPlans;
using AccrediGo.Domain.Entities;
using AutoMapper;

namespace AccrediGo.Application.Mappers.BillingDetails
{
    public class SubscriptionPlanMapping:Profile
    {
        public SubscriptionPlanMapping()
        {
            CreateMap<SubscriptionPlan, ReadAllsubscriptionPlansDTO>();

            // From DTO to Domain for creation
            CreateMap<CreateSubscriptionPlanDTO, SubscriptionPlan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore())
                .ForMember(dest => dest.SubscriptionPlanFeatures, opt => opt.Ignore());
        }
    }
}
