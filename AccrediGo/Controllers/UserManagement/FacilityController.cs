using AccrediGo.Application.Features.UserManagement.Facilities.CreateFacility;
using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.API.Controllers.UserManagement
{
    [Route("api/user-management/facilities")]
    [AllowAnonymous]
    public class FacilityController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public FacilityController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new facility (creates both User and Facility records)
        /// </summary>
        /// <param name="command">The facility creation command containing user and facility details</param>
        /// <returns>The created facility record</returns>
        /// <response code="201">Returns the created facility record</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateFacilityDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Register new facility",
            Description = "Registers a new facility and creates the associated user record",
            OperationId = "RegisterFacility",
            Tags = new[] { "Facility" }
        )]
        public async Task<ActionResult<ApiResponse<CreateFacilityDto>>> Register([FromBody] CreateFacilityCommand command)
        {
            try
            {
                ValidateModelState("FACILITY_CREATE_VALIDATION_ERROR", "Invalid facility data", "بيانات المنشأة غير صالحة");
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(Register), new { id = result.UserId }, ApiResponse<CreateFacilityDto>.Success(result, "Facility Registered Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<CreateFacilityDto>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateFacilityDto>.Error("Failed to register facility", ex));
            }
        }
    }
}
