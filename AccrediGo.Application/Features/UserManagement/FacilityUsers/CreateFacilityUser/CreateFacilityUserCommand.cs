using System.Text.Json.Serialization;
using AccrediGo.Application.Interfaces;
using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.Application.Features.UserManagement.FacilityUsers.CreateFacilityUser
{
    public class CreateFacilityUserCommand : IRequest<CreateFacilityUserDto>, IMapTo<AccrediGo.Domain.Entities.UserDetails.User>, IAuditableCommand
    {
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int SystemRoleId { get; set; } = 2; // Staff member
        public string FacilityId { get; set; } = string.Empty;
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public string? UserAgent { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public string? CreatedFromIp { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string AuditContext { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFacilityUserCommand, AccrediGo.Domain.Entities.UserDetails.User>();
            profile.CreateMap<AccrediGo.Domain.Entities.UserDetails.User, CreateFacilityUserDto>();
        }
    }
}
