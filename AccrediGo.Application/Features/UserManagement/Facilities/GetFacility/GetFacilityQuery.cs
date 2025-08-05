using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Facilities.GetFacility
{
    public class GetFacilityQuery : IRequest<GetFacilityDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
