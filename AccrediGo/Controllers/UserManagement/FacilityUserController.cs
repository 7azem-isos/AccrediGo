using AccrediGo.Application.Features.UserManagement.FacilityUsers.CreateFacilityUser;
using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.API.Controllers.UserManagement
{
    [Route("api/user-management/facility-users")]
    [Authorize(Roles = "1")]
    public class FacilityUserController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public FacilityUserController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new staff member (FacilityUser) for a facility
        /// </summary>
        /// <param name="command">The staff member creation command containing user and facility user details</param>
        /// <returns>The created staff member record</returns>
        /// <response code="201">Returns the created staff member record</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateFacilityUserDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Register new staff member",
            Description = "Registers a new staff member and creates the associated user and facility user records",
            OperationId = "RegisterFacilityUser",
            Tags = new[] { "FacilityUser" }
        )]
        public async Task<ActionResult<ApiResponse<CreateFacilityUserDto>>> Register([FromBody] CreateFacilityUserCommand command)
        {
            try
            {
                ValidateModelState("FACILITY_USER_CREATE_VALIDATION_ERROR", "Invalid staff member data", "بيانات الموظف غير صالحة");
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(Register), new { id = result.UserId }, ApiResponse<CreateFacilityUserDto>.Success(result, "Staff Member Registered Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<CreateFacilityUserDto>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateFacilityUserDto>.Error("Failed to register staff member", ex));
            }
        }
    }
}
