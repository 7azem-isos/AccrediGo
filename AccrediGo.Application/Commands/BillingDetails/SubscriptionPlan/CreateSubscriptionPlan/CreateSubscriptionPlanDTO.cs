using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccrediGo.Application.Commands.BillingDetails.SubscriptionPlan.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanDTO
    {
        public string Type { get; set; } = null!;
        public int Pricing { get; set; }
    }
}
