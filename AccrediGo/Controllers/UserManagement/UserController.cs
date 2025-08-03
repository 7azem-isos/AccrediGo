using AccrediGo.Application.Features.UserManagement.Users.CreateUser;
using AccrediGo.Application.Features.UserManagement.Users.GetAllUsers;
using AccrediGo.Application.Features.UserManagement.Users.GetUserById;
using AccrediGo.Application.Features.UserManagement.Users.UpdateUser;
using AccrediGo.Application.Features.UserManagement.Users.DeleteUser;
using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace AccrediGo.API.Controllers.UserManagement
{
    [Route(AccrediGoRoutes.UserManagement.Users)]
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a paginated list of user records
        /// </summary>
        /// <param name="query">Query parameters for filtering and pagination</param>
        /// <returns>A paginated list of user records</returns>
        /// <response code="200">Returns the paginated list of user records</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to view users</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetAllUsersDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Get all users",
            Description = "Retrieves a paginated list of user records with optional filtering and sorting",
            OperationId = "GetAllUsers",
            Tags = new[] { "User Management" }
        )]
        public async Task<ActionResult<PaginatedResponse<GetAllUsersDto>>> Get([FromQuery] GetAllUsersQuery query)
        {
            try
            {
                // Validate user has permission to view users
                ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to view users",
                    "ليس لديك صلاحية لعرض المستخدمين");

                var paginatedResult = await _mediator.Send(query);

                var paginatedResponse = PaginatedResponse<GetAllUsersDto>.Success(
                    paginatedResult.Result.ToList(),
                    paginatedResult.TotalItemsCount,
                    "Users retrieved successfully"
                );

                return Ok(paginatedResponse);
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<PaginatedResponse<GetAllUsersDto>>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PaginatedResponse<GetAllUsersDto>>.Error("Failed to retrieve users", ex));
            }
        }

        /// <summary>
        /// Retrieves a specific user by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <returns>The user record if found</returns>
        /// <response code="200">Returns the user record</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to view users</response>
        /// <response code="404">If the user is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetUserByIdDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get user by ID",
            Description = "Retrieves a specific user record by their unique identifier",
            OperationId = "GetUserById",
            Tags = new[] { "User Management" }
        )]
        public async Task<ActionResult<ApiResponse<GetUserByIdDto>>> GetById(string id)
        {
            try
            {
                // Validate user has permission to view users
                ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to view users",
                    "ليس لديك صلاحية لعرض المستخدمين");

                ValidateStringNotEmpty(id, "USER_ID_EMPTY_ERROR",
                    "User ID cannot be empty",
                    "معرف المستخدم لا يمكن أن يكون فارغاً");

                var query = new GetUserByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                return Ok(ApiResponse<GetUserByIdDto>.Success(result, "User retrieved successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<GetUserByIdDto>.ValidationError(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<GetUserByIdDto>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GetUserByIdDto>.Error("Failed to retrieve user", ex));
            }
        }

        /// <summary>
        /// Creates a new user record
        /// </summary>
        /// <param name="command">The user creation command containing user details</param>
        /// <returns>The created user record</returns>
        /// <response code="201">Returns the created user record</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to create users</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateUserDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Create new user",
            Description = "Creates a new user record with the provided information",
            OperationId = "CreateUser",
            Tags = new[] { "User Management" }
        )]
        public async Task<ActionResult<ApiResponse<CreateUserDto>>> Create([FromBody] CreateUserCommand command)
        {
            try
            {
                // Validate user has permission to create users
                ValidateCondition(GetCurrentUserRoleId() >= 2, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to create users",
                    "ليس لديك صلاحية لإنشاء المستخدمين");

                ValidateModelState("USER_CREATE_VALIDATION_ERROR", "Invalid user data", "بيانات المستخدم غير صالحة");

                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
                    ApiResponse<CreateUserDto>.Success(result, "User Created Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<CreateUserDto>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateUserDto>.Error("Failed to create user", ex));
            }
        }

        /// <summary>
        /// Updates an existing user record
        /// </summary>
        /// <param name="id">The unique identifier of the user to update</param>
        /// <param name="command">The user update command containing updated user details</param>
        /// <returns>The updated user record</returns>
        /// <response code="200">Returns the updated user record</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to update users</response>
        /// <response code="404">If the user is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UpdateUserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Update user",
            Description = "Updates an existing user record with the provided information",
            OperationId = "UpdateUser",
            Tags = new[] { "User Management" }
        )]
        public async Task<ActionResult<ApiResponse<UpdateUserDto>>> Update(string id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                // Validate user has permission to update users
                ValidateCondition(GetCurrentUserRoleId() >= 2, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to update users",
                    "ليس لديك صلاحية لتحديث المستخدمين");

                ValidateStringNotEmpty(id, "USER_ID_EMPTY_ERROR",
                    "User ID cannot be empty",
                    "معرف المستخدم لا يمكن أن يكون فارغاً");

                ValidateModelState("USER_UPDATE_VALIDATION_ERROR", "Invalid user data", "بيانات المستخدم غير صالحة");

                command.Id = id;
                var result = await _mediator.Send(command);

                return Ok(ApiResponse<UpdateUserDto>.Success(result, "User Updated Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<UpdateUserDto>.ValidationError(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<UpdateUserDto>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UpdateUserDto>.Error("Failed to update user", ex));
            }
        }

        /// <summary>
        /// Deletes a user record
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <returns>Success message indicating the user was deleted</returns>
        /// <response code="200">Returns success message</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user does not have permission to delete users</response>
        /// <response code="404">If the user is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Delete user",
            Description = "Deletes a user record by their unique identifier",
            OperationId = "DeleteUser",
            Tags = new[] { "User Management" }
        )]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
        {
            try
            {
                // Validate user has permission to delete users
                ValidateCondition(GetCurrentUserRoleId() >= 3, "INSUFFICIENT_PERMISSIONS_ERROR",
                    "You don't have permission to delete users",
                    "ليس لديك صلاحية لحذف المستخدمين");

                ValidateStringNotEmpty(id, "USER_ID_EMPTY_ERROR",
                    "User ID cannot be empty",
                    "معرف المستخدم لا يمكن أن يكون فارغاً");

                var command = new DeleteUserCommand { Id = id };
                await _mediator.Send(command);

                return Ok(ApiResponse<bool>.Success(true, "User Deleted Successfully"));
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
                return BadRequest(ApiResponse<bool>.Error("Failed to delete user", ex));
            }
        }
    }
} 