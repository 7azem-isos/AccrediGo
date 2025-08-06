namespace AccrediGo.Application.Features.UserManagement.FacilityUsers.CreateFacilityUser
{
    public class CreateFacilityUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int SystemRoleId { get; set; } = 2; // Staff member
        public string FacilityId { get; set; } = string.Empty;
        // public string? UserAgent { get; set; }
        // public string? CreatedFromIp { get; set; }
    }
}
