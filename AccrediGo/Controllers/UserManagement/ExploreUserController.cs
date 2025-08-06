using AccrediGo.Application.Features.UserManagement.ExploreUsers.CreateExploreUser;
using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.API.Controllers.UserManagement
{
    [Route("api/user-management/explore-users")]
    [Authorize]
    public class ExploreUserController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ExploreUserController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new explore user (creates both User and ExploreUserAccess records)
        /// </summary>
        /// <param name="command">The explore user creation command containing user and access details</param>
        /// <returns>The created explore user record</returns>
        /// <response code="201">Returns the created explore user record</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateExploreUserDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Register new explore user",
            Description = "Registers a new explore user and creates the associated user and access records",
            OperationId = "RegisterExploreUser",
            Tags = new[] { "ExploreUser" }
        )]
        public async Task<ActionResult<ApiResponse<CreateExploreUserDto>>> Register([FromBody] CreateExploreUserCommand command)
        {
            try
            {
                ValidateModelState("EXPLORE_USER_CREATE_VALIDATION_ERROR", "Invalid explore user data", "بيانات المستخدم الاستكشافي غير صالحة");
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(Register), new { id = result.UserId }, ApiResponse<CreateExploreUserDto>.Success(result, "Explore User Registered Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<CreateExploreUserDto>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateExploreUserDto>.Error("Failed to register explore user", ex));
            }
        }
    }
}
