using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.UserManagement.ExploreUsers.GetExploreUser
{
    public class GetExploreUserQueryHandler : IRequestHandler<GetExploreUserQuery, GetExploreUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetExploreUserQueryHandler> _logger;

        public GetExploreUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetExploreUserQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetExploreUserDto> Handle(GetExploreUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving explore user with UserId: {UserId}", request.UserId);
            var exploreUser = await _unitOfWork.GetRepository<ExploreUserAccess>().FirstOrDefaultAsync(eu => eu.UserID == request.UserId, cancellationToken);
            if (exploreUser == null)
                return null;
            // Optionally, fetch User details if needed
            return _mapper.Map<GetExploreUserDto>(exploreUser);
        }
    }
}
