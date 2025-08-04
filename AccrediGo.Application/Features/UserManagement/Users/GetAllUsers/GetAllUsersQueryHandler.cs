using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.UserManagement.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedUsersResult>
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

        public async Task<PaginatedUsersResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllUsersQuery. UserId: {UserId}, CompanyId: {CompanyId}", 
                    _currentRequest?.UserId, _currentRequest?.CompanyId);

                if (request == null)
                {
                    _logger.LogWarning("GetAllUsersQuery request is null.");
                    return new PaginatedUsersResult { Result = new List<GetAllUsersDto>(), TotalItemsCount = 0 };
                }

                // Build predicate for filtering
                var predicate = BuildPredicate(request);

                // Build ordering expression
                var orderBy = BuildOrderByExpression(request.SortBy, request.SortDirection);
                var ascending = request.SortDirection?.ToLower() != "desc";

                // Get paginated results using repository
                var (users, totalCount) = await _unitOfWork.UserRepository.GetPagedAsync(
                    request.PageNumber,
                    request.PageSize,
                    predicate,
                    orderBy,
                    ascending,
                    cancellationToken);

                _logger.LogDebug("Retrieved {Count} users from repository with total count {TotalCount}.", 
                    users.Count(), totalCount);

                // Map to DTOs
                var dtos = _mapper.Map<IEnumerable<GetAllUsersDto>>(users);

                return new PaginatedUsersResult
                {
                    Result = dtos,
                    TotalItemsCount = totalCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllUsersQuery");
                throw;
            }
        }

        private System.Linq.Expressions.Expression<Func<Domain.Entities.UserDetails.User, bool>> BuildPredicate(GetAllUsersQuery request)
        {
            return user =>
                (string.IsNullOrEmpty(request.RoleId) || user.SystemRoleId.ToString() == request.RoleId) &&
                (string.IsNullOrWhiteSpace(request.FreeText) || 
                 user.Name.Contains(request.FreeText.Trim(), StringComparison.OrdinalIgnoreCase) ||
                 user.Email.Contains(request.FreeText.Trim(), StringComparison.OrdinalIgnoreCase) ||
                 (user.PhoneNumber != null && user.PhoneNumber.Contains(request.FreeText.Trim(), StringComparison.OrdinalIgnoreCase)) ||
                 (user.ArabicName != null && user.ArabicName.Contains(request.FreeText.Trim(), StringComparison.OrdinalIgnoreCase)));
        }

        private System.Linq.Expressions.Expression<Func<Domain.Entities.UserDetails.User, object>> BuildOrderByExpression(string sortBy, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return user => user.CreatedAt;
            }

            var sortField = sortBy.ToLower();
            return sortField switch
            {
                "id" => user => user.Id,
                "name" => user => user.Name,
                "email" => user => user.Email,
                "systemroleid" => user => user.SystemRoleId,
                "phonenumber" => user => user.PhoneNumber,
                "arabicname" => user => user.ArabicName,
                "createdat" => user => user.CreatedAt,
                "updatedat" => user => user.UpdatedAt,
                _ => user => user.CreatedAt
            };
        }
    }
} 