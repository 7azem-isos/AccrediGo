using MediatR;

namespace AccrediGo.Application.Features.Authentication.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 