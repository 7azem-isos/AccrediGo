namespace AccrediGo.Application.Features.UserManagement.Facilities.CreateFacility
{
    public class CreateFacilityDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Location { get; set; }
        public string? ArabicLocation { get; set; }
        public int FacilityTypeId { get; set; }
        public string AccreditationId { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Tel { get; set; }
        public int CompanySize { get; set; }
    }
}
