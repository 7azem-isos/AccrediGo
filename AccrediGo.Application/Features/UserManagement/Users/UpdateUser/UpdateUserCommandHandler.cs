using System;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.GetRepository<AccrediGo.Domain.Entities.UserDetails.User>().GetByIdAsync(request.Id, cancellationToken);
            
            if (existingUser == null)
            {
                throw new ArgumentException($"User with ID {request.Id} not found.");
            }

            var entity = _mapper.Map<AccrediGo.Domain.Entities.UserDetails.User>(request);
            
            // Set update timestamp
            entity.UpdatedAt = DateTime.UtcNow;
            
            // Preserve existing password if not being updated
            if (string.IsNullOrEmpty(entity.Password))
            {
                entity.Password = existingUser.Password;
            }

            _unitOfWork.GetRepository<AccrediGo.Domain.Entities.UserDetails.User>().Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateUserDto>(entity);
        }
    }
} 