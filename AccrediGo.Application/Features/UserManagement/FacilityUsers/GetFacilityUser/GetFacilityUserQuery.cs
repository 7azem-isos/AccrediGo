using MediatR;

namespace AccrediGo.Application.Features.UserManagement.FacilityUsers.GetFacilityUser
{
    public class GetFacilityUserQuery : IRequest<GetFacilityUserDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
