using System;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;
using AccreditationEntity = AccrediGo.Domain.Entities.MainComponents.Accreditation;

namespace AccrediGo.Application.Features.Accreditation.Accreditations.CreateAccreditation
{
    public class CreateAccreditationCommandHandler : IRequestHandler<CreateAccreditationCommand, CreateAccreditationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAccreditationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CreateAccreditationDto> Handle(CreateAccreditationCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AccreditationEntity>(request);
            
            // Set ID if not provided
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            
            // Set creation timestamp if not provided
            if (entity.CreatedAt == default)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            await _unitOfWork.GetRepository<AccrediGo.Domain.Entities.MainComponents.Accreditation>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CreateAccreditationDto>(entity);
        }
    }
} 