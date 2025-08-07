using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Application.Interfaces;
using MediatR;

namespace AccrediGo.Application.Features.Authentication.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Get all users and find by email (in a real app, you'd have a specific query)
            var users = await _unitOfWork.GetRepository<AccrediGo.Domain.Entities.UserDetails.User>().GetAllAsync(cancellationToken);
            var user = users.FirstOrDefault(u => u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));


            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            if (!user.IsEmailVerified)
            {
                throw new UnauthorizedAccessException("Please verify your email before logging in.");
            }

            // Use PasswordHasher to verify hashed password
            if (!AccrediGo.Application.Common.PasswordHasher.VerifyPassword(request.Password ?? string.Empty, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var expiresAt = _jwtService.GetExpirationTime();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
                TokenType = "Bearer",
                User = new UserInfo
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    SystemRoleId = user.SystemRoleId,
                    RoleName = "User" // In a real app, you'd get this from the role table
                }
            };
        }
    }
} 