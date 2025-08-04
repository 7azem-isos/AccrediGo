using MediatR;

namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.GetAllSubscriptionPlans
{
    public class GetAllSubscriptionPlansQuery : IRequest<IEnumerable<GetAllSubscriptionPlansDto>>
    {
        // Query parameters can be added here if needed
    }
} 