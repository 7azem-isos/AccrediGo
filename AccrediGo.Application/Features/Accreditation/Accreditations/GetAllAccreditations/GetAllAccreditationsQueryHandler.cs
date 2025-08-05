using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.Accreditation.Accreditations.GetAllAccreditations
{
    public class GetAllAccreditationsQueryHandler : IRequestHandler<GetAllAccreditationsQuery, PaginatedAccreditationsResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentRequest _currentRequest;
        private readonly ILogger<GetAllAccreditationsQueryHandler> _logger;

        public GetAllAccreditationsQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            ICurrentRequest currentRequest,
            ILogger<GetAllAccreditationsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentRequest = currentRequest ?? throw new ArgumentNullException(nameof(currentRequest));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedAccreditationsResult> Handle(GetAllAccreditationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllAccreditationsQuery. UserId: {UserId}, CompanyId: {CompanyId}", 
                    _currentRequest?.UserId, _currentRequest?.CompanyId);

                if (request == null)
                {
                    _logger.LogWarning("GetAllAccreditationsQuery request is null.");
                    return new PaginatedAccreditationsResult { Result = new List<GetAllAccreditationsDto>(), TotalItemsCount = 0 };
                }

                // Build predicate for filtering
                Expression<Func<AccrediGo.Domain.Entities.MainComponents.Accreditation, bool>>? predicate = null;
                
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    predicate = a => a.Name.Contains(request.SearchTerm) || 
                                    (a.ArabicName != null && a.ArabicName.Contains(request.SearchTerm));
                }

                // Build ordering expression
                Expression<Func<AccrediGo.Domain.Entities.MainComponents.Accreditation, object>>? orderBy = request.SortBy?.ToLower() switch
                {
                    "name" => a => a.Name,
                    "arabicname" => a => a.ArabicName ?? string.Empty,
                    "description" => a => a.Description ?? string.Empty,
                    "arabicdescription" => a => a.ArabicDescription ?? string.Empty,
                    _ => a => a.Name // Default ordering
                };

                // Use GenericRepository's GetPagedAsync method for advanced functionality
                var (accreditations, totalCount) = await _unitOfWork.GetRepository<AccrediGo.Domain.Entities.MainComponents.Accreditation>().GetPagedAsync(
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    predicate: predicate,
                    orderBy: orderBy,
                    ascending: request.Ascending,
                    cancellationToken: cancellationToken);

                _logger.LogDebug("Retrieved {Count} accreditations from repository with total count {TotalCount}.", 
                    accreditations.Count(), totalCount);

                // Map to DTOs using AutoMapper
                var dtos = _mapper.Map<IEnumerable<GetAllAccreditationsDto>>(accreditations);

                return new PaginatedAccreditationsResult
                {
                    Result = dtos,
                    TotalItemsCount = totalCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllAccreditationsQuery");
                throw;
            }
        }
    }
} 