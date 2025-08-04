using System.Collections.Generic;

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

    public class PaginatedAccreditationsResult
    {
        public IEnumerable<GetAllAccreditationsDto> Result { get; set; }
        public int TotalItemsCount { get; set; }
    }

    public class PaginatedAccreditationsResponse
    {
        public IEnumerable<GetAllAccreditationsDto> Items { get; set; } = new List<GetAllAccreditationsDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
} 