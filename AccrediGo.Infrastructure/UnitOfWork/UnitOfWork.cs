using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Data;
using AccrediGo.Infrastructure.Repositories;

namespace AccrediGo.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AccrediGoDbContext _context;
        private IGenericCommandRepository<SubscriptionPlan> _subscriptionPlanRepository;
        public UnitOfWork(AccrediGoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IGenericCommandRepository<SubscriptionPlan> SubscriptionPlanRepository
        {
            get
            {
                return _subscriptionPlanRepository ??= new GenericCommandRepository<SubscriptionPlan>(_context);
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
