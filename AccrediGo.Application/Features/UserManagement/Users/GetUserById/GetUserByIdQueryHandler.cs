using System;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // Use CancellationToken in repository operations
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (user == null)
                throw new ArgumentException($"User with ID {request.Id} not found");
            
            return _mapper.Map<GetUserByIdDto>(user);
        }
    }
} 