using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, CreateSubscriptionPlanDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSubscriptionPlanCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateSubscriptionPlanDto> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AccrediGo.Domain.Entities.BillingDetails.SubscriptionPlan>(request);
            
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

            await _unitOfWork.GetRepository<AccrediGo.Domain.Entities.BillingDetails.SubscriptionPlan>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CreateSubscriptionPlanDto>(entity);
        }
    }
} 