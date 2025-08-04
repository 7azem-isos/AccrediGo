using AccrediGo.Application.Interfaces;

namespace AccrediGo.Application.Features.Accreditation.Accreditations.CreateAccreditation
{
    public class CreateAccreditationCommand : ICreateCommand<CreateAccreditationDto>
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
        public string CreatedFromIp { get; set; }
        public string UserAgent { get; set; }
        public string AuditContext { get; set; }

        public CreateAccreditationCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
} 