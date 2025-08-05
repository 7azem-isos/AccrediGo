namespace AccrediGo.Application.Features.UserManagement.FacilityUsers.GetFacilityUser
{
    public class GetFacilityUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FacilityUserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int SystemRoleId { get; set; } = 2;
        public string? UserAgent { get; set; }
        public string? CreatedFromIp { get; set; }
    }
}
