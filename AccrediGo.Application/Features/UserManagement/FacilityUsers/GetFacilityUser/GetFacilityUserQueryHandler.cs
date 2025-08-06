using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.UserManagement.FacilityUsers.GetFacilityUser
{
    public class GetFacilityUserQueryHandler : IRequestHandler<GetFacilityUserQuery, GetFacilityUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetFacilityUserQueryHandler> _logger;

        public GetFacilityUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetFacilityUserQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetFacilityUserDto> Handle(GetFacilityUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving facility user with UserId: {UserId}", request.UserId);
            var facilityUser = await _unitOfWork.GetRepository<FacilityUser>().FirstOrDefaultAsync(fu => fu.UserID == request.UserId, cancellationToken);
            if (facilityUser == null)
                return null;
            return _mapper.Map<GetFacilityUserDto>(facilityUser);
        }
    }
}
