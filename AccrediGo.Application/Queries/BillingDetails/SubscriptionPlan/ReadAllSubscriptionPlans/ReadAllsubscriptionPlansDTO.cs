using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccrediGo.Application.Queries.BillingDetails.SubscriptionPlan.ReadAllSubscriptionPlans
{
    public class ReadAllsubscriptionPlansDTO
    {
        public string Id { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Pricing { get; set; }
    }
}
