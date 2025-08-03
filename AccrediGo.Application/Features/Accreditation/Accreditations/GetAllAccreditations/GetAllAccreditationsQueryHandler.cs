using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Features.Accreditation.Accreditations.GetAllAccreditations
{
    public class GetAllAccreditationsQueryHandler : IRequestHandler<GetAllAccreditationsQuery, IEnumerable<GetAllAccreditationsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAccreditationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<GetAllAccreditationsDto>> Handle(GetAllAccreditationsQuery request, CancellationToken cancellationToken)
        {
            var accreditations = await _unitOfWork.AccreditationQueryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllAccreditationsDto>>(accreditations);
        }
    }
} 