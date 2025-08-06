using AccrediGo.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Features.UserManagement.ExploreUsers.CreateExploreUser
{
    public class CreateExploreUserCommand : IRequest<CreateExploreUserDto>, IMapTo<AccrediGo.Domain.Entities.UserDetails.User>, IAuditableCommand
    {
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int SystemRoleId { get; set; } = 3; // Explore user
        // public string? UserAgent { get; set; }
        // public string? CreatedFromIp { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string AuditContext { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateExploreUserCommand, AccrediGo.Domain.Entities.UserDetails.User>();
            profile.CreateMap<AccrediGo.Domain.Entities.UserDetails.User, CreateExploreUserDto>();
        }
    }
}
