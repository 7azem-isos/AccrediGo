using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.BaseModels;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccrediGo.Infrastructure.Repositories
{
    public class GenericCommandRepository<T> : IGenericCommandRepository<T> where T : BaseEntity
    {
        private readonly AccrediGoDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericCommandRepository(AccrediGoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            await _dbSet.AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
            }

            _dbSet.UpdateRange(entities);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }
    }
}
