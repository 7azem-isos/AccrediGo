using System;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using MediatR;

namespace AccrediGo.Application.Features.UserManagement.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.GetRepository<AccrediGo.Domain.Entities.UserDetails.User>().GetByIdAsync(request.Id, cancellationToken);
            
            if (existingUser == null)
            {
                throw new ArgumentException($"User with ID {request.Id} not found.");
            }

            _unitOfWork.GetRepository<AccrediGo.Domain.Entities.UserDetails.User>().Remove(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
} 