using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccrediGo.Infrastructure.Repositories
{
    public class GenericQueryRepository<TEntity> : IGenericQueryRepository<TEntity> where TEntity : class
    {
        private readonly AccrediGoDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericQueryRepository(AccrediGoDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
