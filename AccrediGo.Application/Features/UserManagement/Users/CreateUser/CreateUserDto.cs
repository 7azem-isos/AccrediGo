namespace AccrediGo.Application.Features.UserManagement.Users.CreateUser
{
    public class CreateUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string Email { get; set; } = string.Empty;
        public int SystemRoleId { get; set; }
        public string? PhoneNumber { get; set; }
    }
} 