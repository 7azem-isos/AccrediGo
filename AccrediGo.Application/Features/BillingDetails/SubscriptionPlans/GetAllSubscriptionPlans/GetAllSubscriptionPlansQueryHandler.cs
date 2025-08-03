using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.GetAllSubscriptionPlans
{
    public class GetAllSubscriptionPlansQueryHandler : IRequestHandler<GetAllSubscriptionPlansQuery, IEnumerable<GetAllSubscriptionPlansDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSubscriptionPlansQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<GetAllSubscriptionPlansDto>> Handle(GetAllSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            // Note: This requires adding IGenericQueryRepository<SubscriptionPlan> to IUnitOfWork
            // For now, we'll use a placeholder implementation
            throw new NotImplementedException("Query repository not yet implemented in IUnitOfWork");
        }
    }
} 