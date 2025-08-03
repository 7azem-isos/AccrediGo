using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Users.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetAllUsersDto>>
    {
        public string RoleId { get; set; }
        public string Status { get; set; }
        public string FreeText { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
} 