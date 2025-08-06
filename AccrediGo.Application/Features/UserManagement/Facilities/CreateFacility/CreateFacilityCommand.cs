using AccrediGo.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Facilities.CreateFacility
{
public class CreateFacilityCommand : IRequest<CreateFacilityDto>, IMapTo<AccrediGo.Domain.Entities.MainComponents.Facility>
    {
        // User fields
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Tel { get; set; }
        public int SystemRoleId { get; set; } = 1; // Facility role
        public int CompanySize { get; set; }

        // Facility fields
        public string Location { get; set; } = string.Empty;
        public string? ArabicLocation { get; set; }
        public int FacilityTypeId { get; set; }
        public string AccreditationId { get; set; } = string.Empty;

        // Audit fields
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // public string CreatedFromIp { get; set; } = string.Empty;
        // public string UserAgent { get; set; } = string.Empty;
        // public string AuditContext { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFacilityCommand, AccrediGo.Domain.Entities.MainComponents.Facility>();
            profile.CreateMap<AccrediGo.Domain.Entities.MainComponents.Facility, CreateFacilityDto>();
        }
    }
}
