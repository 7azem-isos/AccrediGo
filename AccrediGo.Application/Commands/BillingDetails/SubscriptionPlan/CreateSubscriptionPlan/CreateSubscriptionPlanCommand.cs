using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AccrediGo.Application.Commands.BillingDetails.SubscriptionPlan.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommand:IRequest<string>
    {
        public CreateSubscriptionPlanDTO SubscriptionPlan { get; set; } = null!;
        public CreateSubscriptionPlanCommand(CreateSubscriptionPlanDTO subscriptionPlan)
        {
            SubscriptionPlan = subscriptionPlan;
        }

    }
}
