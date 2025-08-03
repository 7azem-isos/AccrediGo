using AccrediGo.Application.Commands.BillingDetails.SubscriptionPlan.CreateSubscriptionPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccrediGo.API.Controllers.BillingDetails
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanController : Controller
    {
        private readonly IMediator _mediator;

        public SubscriptionPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanDTO dto)
        {
            var result = await _mediator.Send(new CreateSubscriptionPlanCommand(dto));
            return Ok(new { id = result, message = "Subscription Plan Created Successfully" });
        }
    }
}
