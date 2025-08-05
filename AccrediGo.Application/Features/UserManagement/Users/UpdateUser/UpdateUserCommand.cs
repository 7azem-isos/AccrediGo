using AccrediGo.Domain.Entities.UserDetails;
using MediatR;
using AutoMapper;
using AccrediGo.Application.Interfaces;

namespace AccrediGo.Application.Features.UserManagement.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserDto>, IMapTo<User>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string Email { get; set; } = string.Empty;
        public int SystemRoleId { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserCommand, User>();
            profile.CreateMap<User, UpdateUserDto>();
        }
    }
} 