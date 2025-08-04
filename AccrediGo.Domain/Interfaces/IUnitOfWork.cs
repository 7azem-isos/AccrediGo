using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BillingDetails;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.UserDetails;

namespace AccrediGo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<SubscriptionPlan> SubscriptionPlanRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Accreditation> AccreditationRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
