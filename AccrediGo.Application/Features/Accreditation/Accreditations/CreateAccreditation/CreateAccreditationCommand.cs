using System.Text.Json.Serialization;
using AccrediGo.Application.Interfaces;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;

namespace AccrediGo.Application.Features.Accreditation.Accreditations.CreateAccreditation
{
    public class CreateAccreditationCommand : ICreateCommand<CreateAccreditationDto>, IMapTo<AccrediGo.Domain.Entities.MainComponents.Accreditation>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Standard { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string Status { get; set; }

        // Audit properties (implemented from IAuditableCommand)
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public string CreatedFromIp { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public string UserAgent { get; set; }
        public string AuditContext { get; set; }

        public CreateAccreditationCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAccreditationCommand, AccrediGo.Domain.Entities.MainComponents.Accreditation>();
            profile.CreateMap<AccrediGo.Domain.Entities.MainComponents.Accreditation, CreateAccreditationDto>();
        }
    }
} 