using MediatR;

namespace AccrediGo.Application.Features.Accreditation.Accreditations.GetAllAccreditations
{
    public class GetAllAccreditationsQuery : IRequest<PaginatedAccreditationsResult>
    {
        /// <summary>
        /// Optional search term to filter accreditations by name
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Whether to include only active accreditations
        /// </summary>
        public bool ActiveOnly { get; set; } = true;

        /// <summary>
        /// Sort by field (Name, ArabicName, etc.)
        /// </summary>
        public string? SortBy { get; set; } = "Name";

        /// <summary>
        /// Sort direction (true for ascending, false for descending)
        /// </summary>
        public bool Ascending { get; set; } = true;
    }
} 