using AccrediGo.Application.Interfaces;

namespace AccrediGo.Application.Features.UserManagement.Users.CreateUser
{
    public class CreateUserCommand : ICreateCommand<CreateUserDto>
    {
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int SystemRoleId { get; set; }
        public string PhoneNumber { get; set; }

        // Audit properties (implemented from IAuditableCommand)
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedFromIp { get; set; }
        public string UserAgent { get; set; }
        public string AuditContext { get; set; }

        public CreateUserCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
} 