using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.API.Controllers.UserManagement
{
    [Route(AccrediGoRoutes.UserManagement.UserProfiles)]
    [Authorize]
    public class UserProfileController : ApiControllerBase
    {
        public UserProfileController(ICurrentRequest currentRequest) : base(currentRequest)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var profile = new
                {
                    Id = GetCurrentUserId(),
                    Name = GetCurrentUserName(),
                    Email = GetCurrentUserEmail(),
                    RoleId = GetCurrentUserRoleId(),
                    Language = GetCurrentLanguage(),
                    CorrelationId = GetCorrelationId()
                };

                return Ok(ApiResponse<object>.Success(profile, "Profile Retrieved Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Error("Failed to retrieve profile", ex));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] object profileData)
        {
            try
            {
                ValidateModelState("PROFILE_UPDATE_VALIDATION_ERROR", 
                    "Invalid profile data", 
                    "بيانات الملف الشخصي غير صالحة");

                // Implementation would go here
                return Ok(ApiResponse<object>.Success(profileData, "Profile Updated Successfully"));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ApiResponse<object>.ValidationError(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Error("Failed to update profile", ex));
            }
        }
    }
} 