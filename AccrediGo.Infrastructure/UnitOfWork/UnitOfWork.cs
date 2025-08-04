using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BillingDetails;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Data;
using AccrediGo.Infrastructure.Repositories;
using AutoMapper;

namespace AccrediGo.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccrediGoDbContext _context;
        private readonly IMapper _mapper;
        private IGenericRepository<SubscriptionPlan> _subscriptionPlanRepository;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Accreditation> _accreditationRepository;
        private bool _disposed = false;

        public UnitOfWork(AccrediGoDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IGenericRepository<SubscriptionPlan> SubscriptionPlanRepository
        {
            get { return _subscriptionPlanRepository ??= new GenericRepository<SubscriptionPlan>(_context, _mapper); }
        }

        public IGenericRepository<User> UserRepository
        {
            get { return _userRepository ??= new GenericRepository<User>(_context, _mapper); }
        }

        public IGenericRepository<Accreditation> AccreditationRepository
        {
            get { return _accreditationRepository ??= new GenericRepository<Accreditation>(_context, _mapper); }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context?.Dispose();
            }
            _disposed = true;
        }
    }
}
