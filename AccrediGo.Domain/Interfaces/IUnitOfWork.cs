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
        IGenericRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
