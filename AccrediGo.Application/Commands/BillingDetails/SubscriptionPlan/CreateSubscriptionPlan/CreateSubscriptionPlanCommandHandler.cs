using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Commands.BillingDetails.SubscriptionPlan.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSubscriptionPlanCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<string> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AccrediGo.Domain.Entities.SubscriptionPlan>(request.SubscriptionPlan);
            entity.Id = Guid.NewGuid().ToString(); // ID assignment
            entity.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.SubscriptionPlanRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return entity.Id;
        }
    }
    
}
