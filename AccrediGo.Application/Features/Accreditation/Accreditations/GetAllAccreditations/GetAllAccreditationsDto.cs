namespace AccrediGo.Application.Features.Accreditation.Accreditations.GetAllAccreditations
{
    public class GetAllAccreditationsDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ArabicName { get; set; }
        public string? Description { get; set; }
        public string? ArabicDescription { get; set; }
    }
} 