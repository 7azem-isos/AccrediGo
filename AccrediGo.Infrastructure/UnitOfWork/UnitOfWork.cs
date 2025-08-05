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
        private readonly Dictionary<Type, object> _repositories = new();
        private bool _disposed = false;


        public UnitOfWork(AccrediGoDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new GenericRepository<T>(_context, _mapper);
            return (IGenericRepository<T>)_repositories[type];
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
