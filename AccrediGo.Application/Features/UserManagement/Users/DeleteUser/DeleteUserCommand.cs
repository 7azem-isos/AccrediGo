using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }
} 