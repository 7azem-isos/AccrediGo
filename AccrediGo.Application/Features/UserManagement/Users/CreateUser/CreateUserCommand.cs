
using System.Text.Json.Serialization;
using AccrediGo.Application.Interfaces;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;

namespace AccrediGo.Application.Features.UserManagement.Users.CreateUser
{
public class CreateUserCommand : ICreateCommand<CreateUserDto>, IMapTo<AccrediGo.Domain.Entities.UserDetails.User>, IAuditableCommand
    {

        public string? Name { get; set; }
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int SystemRoleId { get; set; }
        public string? PhoneNumber { get; set; }

        // Audit properties (implemented from IAuditableCommand)
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public string CreatedFromIp { get; set; } = string.Empty;
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public string UserAgent { get; set; } = string.Empty;
        public string AuditContext { get; set; } = string.Empty;

        public CreateUserCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCommand, AccrediGo.Domain.Entities.UserDetails.User>();
            profile.CreateMap<AccrediGo.Domain.Entities.UserDetails.User, CreateUserDto>();
        }
    }
} 