using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.UserManagement.Facilities.GetFacility
{
    public class GetFacilityQueryHandler : IRequestHandler<GetFacilityQuery, GetFacilityDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetFacilityQueryHandler> _logger;

        public GetFacilityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetFacilityQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetFacilityDto> Handle(GetFacilityQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving facility with UserId: {UserId}", request.UserId);
            var facility = await _unitOfWork.GetRepository<Facility>().FirstOrDefaultAsync(f => f.UserId == request.UserId, cancellationToken);
            if (facility == null)
                return null;
            return _mapper.Map<GetFacilityDto>(facility);
        }
    }
}
