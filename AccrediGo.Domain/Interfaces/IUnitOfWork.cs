using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities;
using AccrediGo.Domain.Entities.MainComponents;

namespace AccrediGo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericCommandRepository<SubscriptionPlan> SubscriptionPlanRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
