using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan;
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.GetAllSubscriptionPlans;
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.UpdateSubscriptionPlan;
using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccrediGo.API.Controllers.BillingDetails
{
    [Route(AccrediGoRoutes.BillingDetails.SubscriptionPlans)]
    [Authorize]
    public class SubscriptionPlanController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionPlanController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanCommand command)
        {
            try
            {
                // Validate model state
                ValidateModelState("SUBSCRIPTION_PLAN_CREATE_VALIDATION_ERROR", "Invalid subscription plan data", "بيانات خطة الاشتراك غير صالحة");

                var result = await _mediator.Send(command);
                return Ok(ApiResponse<CreateSubscriptionPlanDto>.Success(result, "Subscription Plan Created Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<CreateSubscriptionPlanDto>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateSubscriptionPlanDto>.Error("Failed to create subscription plan", ex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllSubscriptionPlansQuery();
                var result = await _mediator.Send(query);
                return Ok(ApiResponse<IEnumerable<GetAllSubscriptionPlansDto>>.Success(result, "Subscription Plans Retrieved Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IEnumerable<GetAllSubscriptionPlansDto>>.Error("Failed to retrieve subscription plans", ex));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSubscriptionPlanCommand command)
        {
            try
            {
                command.Id = id;
                var result = await _mediator.Send(command);
                return Ok(ApiResponse<UpdateSubscriptionPlanDto>.Success(result, "Subscription Plan Updated Successfully"));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<UpdateSubscriptionPlanDto>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UpdateSubscriptionPlanDto>.Error("Failed to update subscription plan", ex));
            }
        }
    }
}
