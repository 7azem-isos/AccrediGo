using MediatR;

namespace AccrediGo.Application.Features.UserManagement.ExploreUsers.GetExploreUser
{
    public class GetExploreUserQuery : IRequest<GetExploreUserDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
