using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Models.Common;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.UserManagement.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentRequest _currentRequest;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentRequest currentRequest,
            ILogger<GetAllUsersQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentRequest = currentRequest ?? throw new ArgumentNullException(nameof(currentRequest));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllUsersQuery. UserId: {UserId}, CompanyId: {CompanyId}", 
                    _currentRequest?.UserId, _currentRequest?.CompanyId);

                if (request == null)
                {
                    _logger.LogWarning("GetAllUsersQuery request is null.");
                    return new List<GetAllUsersDto>();
                }

                // Get all users with CancellationToken support
                var users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);
                _logger.LogDebug("Retrieved {Count} users from repository.", users.Count());

                // Apply additional filters if provided
                var filteredUsers = users;
                if (!string.IsNullOrEmpty(request.RoleId))
                {
                    filteredUsers = filteredUsers.Where(u => u.SystemRoleId.ToString() == request.RoleId);
                    _logger.LogDebug("Applied role filter for RoleId: {RoleId}", request.RoleId);
                }

                // Apply free text search
                if (!string.IsNullOrWhiteSpace(request.FreeText))
                {
                    var search = request.FreeText.Trim().ToLower();
                    filteredUsers = filteredUsers.Where(u => 
                        u.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        u.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (u.ArabicName != null && u.ArabicName.Contains(search, StringComparison.OrdinalIgnoreCase)));
                    _logger.LogDebug("Applied free text search for: {SearchTerm}", request.FreeText);
                }

                // Apply sorting
                var sortedUsers = ApplySorting(filteredUsers, request.SortBy, request.SortDirection);

                // Apply pagination
                var paginatedUsers = ApplyPagination(sortedUsers, request.PageNumber, request.PageSize);

                // Map to DTOs
                var dtos = _mapper.Map<IEnumerable<GetAllUsersDto>>(paginatedUsers);

                _logger.LogDebug("Retrieved {Count} users after filtering and pagination.", dtos.Count());
                return dtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllUsersQuery");
                throw;
            }
        }

        private IQueryable<Domain.Entities.UserDetails.User> ApplySorting(IQueryable<Domain.Entities.UserDetails.User> query, string sortBy, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return query.OrderByDescending(u => u.CreatedAt);
            }

            var isDescending = sortDirection?.ToLower() == "desc";
            var sortField = sortBy.ToLower();

            // Define valid sort fields
            var validSortFields = new[] { "id", "name", "email", "systemroleid", "phonenumber", "arabicname", "createdat", "updatedat" };

            if (!validSortFields.Contains(sortField))
            {
                _logger.LogWarning("Invalid SortBy field: {SortBy}. Defaulting to CreatedAt DESC.", sortBy);
                return query.OrderByDescending(u => u.CreatedAt);
            }

            return sortField switch
            {
                "id" => isDescending ? query.OrderByDescending(u => u.Id) : query.OrderBy(u => u.Id),
                "name" => isDescending ? query.OrderByDescending(u => u.Name) : query.OrderBy(u => u.Name),
                "email" => isDescending ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                "systemroleid" => isDescending ? query.OrderByDescending(u => u.SystemRoleId) : query.OrderBy(u => u.SystemRoleId),
                "phonenumber" => isDescending ? query.OrderByDescending(u => u.PhoneNumber) : query.OrderBy(u => u.PhoneNumber),
                "arabicname" => isDescending ? query.OrderByDescending(u => u.ArabicName) : query.OrderBy(u => u.ArabicName),
                "createdat" => isDescending ? query.OrderByDescending(u => u.CreatedAt) : query.OrderBy(u => u.CreatedAt),
                "updatedat" => isDescending ? query.OrderByDescending(u => u.UpdatedAt) : query.OrderBy(u => u.UpdatedAt),
                _ => query.OrderByDescending(u => u.CreatedAt)
            };
        }

        private IEnumerable<Domain.Entities.UserDetails.User> ApplyPagination(IQueryable<Domain.Entities.UserDetails.User> query, int pageNumber, int pageSize)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(1, Math.Min(100, pageSize)); // Limit page size to 100

            _logger.LogDebug("Applying pagination: PageNumber={PageNumber}, PageSize={PageSize}", pageNumber, pageSize);
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 