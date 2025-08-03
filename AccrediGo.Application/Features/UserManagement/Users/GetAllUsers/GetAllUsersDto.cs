using System.Collections.Generic;

namespace AccrediGo.Application.Features.UserManagement.Users.GetAllUsers
{
    public class GetAllUsersDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ArabicName { get; set; }
        public int SystemRoleId { get; set; }
        public string SystemRoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class PaginatedUsersResult
    {
        public IEnumerable<GetAllUsersDto> Result { get; set; }
        public int TotalItemsCount { get; set; }
    }
} 