using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Users.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdDto>
    {
        public string Id { get; set; } = string.Empty;
    }
} 