namespace AccrediGo.Application.Features.UserManagement.ExploreUsers.CreateExploreUser
{
    public class CreateExploreUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int SystemRoleId { get; set; } = 3; // Explore user
        // public string? UserAgent { get; set; }
        // public string? CreatedFromIp { get; set; }
    }
}
