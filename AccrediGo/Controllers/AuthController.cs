using AccrediGo.Application.Features.Authentication.Login;
using AccrediGo.Models.Auth;
using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.Controllers
{
    [Route(AccrediGoRoutes.Auth.Base)]
    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Validate model state
                ValidateModelState("LOGIN_VALIDATION_ERROR", "Invalid login data", "بيانات تسجيل الدخول غير صالحة");

                var command = new LoginCommand
                {
                    Email = request.Email,
                    Password = request.Password
                };

                var result = await _mediator.Send(command);
                var loginResponse = new AccrediGo.Models.Auth.LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken,
                    ExpiresAt = result.ExpiresAt,
                    TokenType = result.TokenType,
                    User = new AccrediGo.Models.Auth.UserInfo
                    {
                        Id = result.User.Id,
                        Name = result.User.Name,
                        Email = result.User.Email,
                        SystemRoleId = result.User.SystemRoleId,
                        RoleName = result.User.RoleName
                    }
                };
                return Ok(ApiResponse<AccrediGo.Models.Auth.LoginResponse>.Success(loginResponse, "Login successful"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<AccrediGo.Models.Auth.LoginResponse>.ValidationError(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<AccrediGo.Models.Auth.LoginResponse>.Unauthorized(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<AccrediGo.Models.Auth.LoginResponse>.Error("Login failed", ex));
            }
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            // This would typically validate the refresh token and generate a new access token
            // For now, we'll return a simple response
            return Ok(ApiResponse<string>.Success("Token refresh endpoint - implement refresh logic", "Refresh token endpoint"));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            var userInfo = new
            {
                Id = userId,
                Name = userName,
                Email = userEmail,
                Role = userRole
            };

            return Ok(ApiResponse<object>.Success(userInfo, "Current user information retrieved successfully"));
        }
    }
} 