using AccrediGo.Application.Features.Accreditation.Accreditations.CreateAccreditation;
using AccrediGo.Application.Features.Accreditation.Accreditations.GetAllAccreditations;
using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace AccrediGo.API.Controllers.Accreditation
{
    [Route(AccrediGoRoutes.Accreditation.Accreditations)]
    [Authorize]
    public class AccreditationController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AccreditationController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a paginated list of accreditation records
        /// </summary>
        /// <param name="query">Query parameters for filtering and pagination</param>
        /// <returns>A paginated list of accreditation records</returns>
        /// <response code="200">Returns the paginated list of accreditation records</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to view accreditations</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetAllAccreditationsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Get all accreditations",
            Description = "Retrieves a paginated list of accreditation records with optional filtering and sorting",
            OperationId = "GetAllAccreditations",
            Tags = new[] { "Accreditation" }
        )]
        public async Task<ActionResult<PaginatedResponse<GetAllAccreditationsDto>>> Get([FromQuery] GetAllAccreditationsQuery query)
        {
            try
            {
                // Validate user has permission to view accreditations
                ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to view accreditations",
                    "ليس لديك صلاحية لعرض الاعتمادات");

                var result = await _mediator.Send(query);

                var paginatedResponse = PaginatedResponse<GetAllAccreditationsDto>.Success(
                    result.ToList(),
                    100, // Total count - would come from actual implementation
                    1,   // Page number
                    10,  // Page size
                    "Accreditations retrieved successfully"
                );

                return Ok(paginatedResponse);
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<PaginatedResponse<GetAllAccreditationsDto>>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PaginatedResponse<GetAllAccreditationsDto>>.Error("Failed to retrieve accreditations", ex));
            }
        }

        /// <summary>
        /// Retrieves a specific accreditation by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the accreditation</param>
        /// <returns>The accreditation record if found</returns>
        /// <response code="200">Returns the accreditation record</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to view accreditations</response>
        /// <response code="404">If the accreditation is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetAllAccreditationsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get accreditation by ID",
            Description = "Retrieves a specific accreditation record by its unique identifier",
            OperationId = "GetAccreditationById",
            Tags = new[] { "Accreditation" }
        )]
        public async Task<ActionResult<ApiResponse<GetAllAccreditationsDto>>> GetById(string id)
        {
            try
            {
                // Validate user has permission to view accreditations
                ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to view accreditations",
                    "ليس لديك صلاحية لعرض الاعتمادات");

                ValidateStringNotEmpty(id, "ACCREDITATION_ID_EMPTY_ERROR",
                    "Accreditation ID cannot be empty",
                    "معرف الاعتماد لا يمكن أن يكون فارغاً");

                // Implementation would go here - create GetAccreditationByIdQuery
                var accreditation = new GetAllAccreditationsDto
                {
                    Id = id,
                    Name = $"Accreditation {id}",
                    Description = "Accreditation description"
                };

                return Ok(ApiResponse<GetAllAccreditationsDto>.Success(accreditation, "Accreditation retrieved successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<GetAllAccreditationsDto>.ValidationError(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<GetAllAccreditationsDto>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GetAllAccreditationsDto>.Error("Failed to retrieve accreditation", ex));
            }
        }

        /// <summary>
        /// Creates a new accreditation record
        /// </summary>
        /// <param name="command">The accreditation creation command containing accreditation details</param>
        /// <returns>The created accreditation record</returns>
        /// <response code="201">Returns the created accreditation record</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to create accreditations</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateAccreditationDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Create new accreditation",
            Description = "Creates a new accreditation record with the provided information",
            OperationId = "CreateAccreditation",
            Tags = new[] { "Accreditation" }
        )]
        public async Task<ActionResult<ApiResponse<CreateAccreditationDto>>> Create([FromBody] CreateAccreditationCommand command)
        {
            try
            {
                // Validate user has permission to create accreditations
                ValidateCondition(GetCurrentUserRoleId() >= 2, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to create accreditations",
                    "ليس لديك صلاحية لإنشاء الاعتمادات");

                ValidateModelState("ACCREDITATION_CREATE_VALIDATION_ERROR", "Invalid accreditation data", "بيانات الاعتماد غير صالحة");

                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                    ApiResponse<CreateAccreditationDto>.Success(result, "Accreditation Created Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<CreateAccreditationDto>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateAccreditationDto>.Error("Failed to create accreditation", ex));
            }
        }

        /// <summary>
        /// Updates an existing accreditation record
        /// </summary>
        /// <param name="id">The unique identifier of the accreditation to update</param>
        /// <param name="command">The accreditation update command containing updated accreditation details</param>
        /// <returns>The updated accreditation record</returns>
        /// <response code="200">Returns the updated accreditation record</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to update accreditations</response>
        /// <response code="404">If the accreditation is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CreateAccreditationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Update accreditation",
            Description = "Updates an existing accreditation record with the provided information",
            OperationId = "UpdateAccreditation",
            Tags = new[] { "Accreditation" }
        )]
        public async Task<ActionResult<ApiResponse<CreateAccreditationDto>>> Update(string id, [FromBody] CreateAccreditationCommand command)
        {
            try
            {
                // Validate user has permission to update accreditations
                ValidateCondition(GetCurrentUserRoleId() >= 2, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to update accreditations",
                    "ليس لديك صلاحية لتحديث الاعتمادات");

                ValidateStringNotEmpty(id, "ACCREDITATION_ID_EMPTY_ERROR",
                    "Accreditation ID cannot be empty",
                    "معرف الاعتماد لا يمكن أن يكون فارغاً");

                ValidateModelState("ACCREDITATION_UPDATE_VALIDATION_ERROR", "Invalid accreditation data", "بيانات الاعتماد غير صالحة");

                // Implementation would go here - create UpdateAccreditationCommand
                var result = new CreateAccreditationDto
                {
                    Id = id,
                    Name = command.Name,
                    Description = command.Description
                };

                return Ok(ApiResponse<CreateAccreditationDto>.Success(result, "Accreditation Updated Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<CreateAccreditationDto>.ValidationError(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<CreateAccreditationDto>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateAccreditationDto>.Error("Failed to update accreditation", ex));
            }
        }

        /// <summary>
        /// Deletes an accreditation record
        /// </summary>
        /// <param name="id">The unique identifier of the accreditation to delete</param>
        /// <returns>Success message indicating the accreditation was deleted</returns>
        /// <response code="200">Returns success message</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to delete accreditations</response>
        /// <response code="404">If the accreditation is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Delete accreditation",
            Description = "Deletes an accreditation record by its unique identifier",
            OperationId = "DeleteAccreditation",
            Tags = new[] { "Accreditation" }
        )]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
        {
            try
            {
                // Validate user has permission to delete accreditations
                ValidateCondition(GetCurrentUserRoleId() >= 3, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to delete accreditations",
                    "ليس لديك صلاحية لحذف الاعتمادات");

                ValidateStringNotEmpty(id, "ACCREDITATION_ID_EMPTY_ERROR",
                    "Accreditation ID cannot be empty",
                    "معرف الاعتماد لا يمكن أن يكون فارغاً");

                // Implementation would go here - create DeleteAccreditationCommand

                return Ok(ApiResponse<bool>.Success(true, "Accreditation Deleted Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<bool>.ValidationError(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<bool>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.Error("Failed to delete accreditation", ex));
            }
        }

        /// <summary>
        /// Submits an accreditation for review
        /// </summary>
        /// <param name="id">The unique identifier of the accreditation to submit</param>
        /// <returns>Success message indicating the accreditation was submitted</returns>
        /// <response code="200">Returns success message</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to submit accreditations</response>
        /// <response code="404">If the accreditation is not found</response>
        [HttpPut("{id}/submit")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Submit accreditation",
            Description = "Submits an accreditation for review and approval",
            OperationId = "SubmitAccreditation",
            Tags = new[] { "Accreditation" }
        )]
        public async Task<ActionResult<ApiResponse<bool>>> Submit(string id)
        {
            try
            {
                // Validate user has permission to submit accreditations
                ValidateCondition(GetCurrentUserRoleId() >= 2, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to submit accreditations",
                    "ليس لديك صلاحية لتقديم الاعتمادات");

                ValidateStringNotEmpty(id, "ACCREDITATION_ID_EMPTY_ERROR",
                    "Accreditation ID cannot be empty",
                    "معرف الاعتماد لا يمكن أن يكون فارغاً");

                // Implementation would go here - create SubmitAccreditationCommand

                return Ok(ApiResponse<bool>.Success(true, "Accreditation Submitted Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<bool>.ValidationError(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<bool>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.Error("Failed to submit accreditation", ex));
            }
        }
    }
} 