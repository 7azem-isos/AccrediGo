using AccrediGo.Domain.Entities.BaseModels;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccrediGo.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AccrediGoDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AccrediGoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        private static PropertyInfo? GetKeyProperty()
        {
            // Look for a property with [Key] attribute
            var keyProp = typeof(TEntity)
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
            return keyProp;
        }

        public async Task<TEntity> GetByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var keyProp = GetKeyProperty();
            if (keyProp == null)
                throw new InvalidOperationException($"No [Key] property found on {typeof(TEntity).Name}");

            // Build a lambda expression for the key comparison
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, keyProp);
            var idValue = Convert.ChangeType(id, keyProp.PropertyType);
            var equals = Expression.Equal(property, Expression.Constant(idValue));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return await query.FirstOrDefaultAsync(lambda)
                ?? throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with key {id} not found.");
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(filter).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var keyProp = GetKeyProperty();
            if (keyProp == null)
                throw new InvalidOperationException($"No [Key] property found on {typeof(TEntity).Name}");

            // Optionally: Check if key is set (for non-identity keys)
            var keyValue = keyProp.GetValue(entity);
            if (keyValue == null || (keyProp.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)keyValue)))
                throw new ArgumentException($"Key property '{keyProp.Name}' must be set before adding entity.");

            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
            var keyProp = GetKeyProperty();
            if (keyProp == null)
                throw new InvalidOperationException($"No [Key] property found on {typeof(TEntity).Name}");

            foreach (var entity in entities)
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                var keyValue = keyProp.GetValue(entity);
                if (keyValue == null || (keyProp.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)keyValue)))
                    throw new ArgumentException($"Key property '{keyProp.Name}' must be set before adding entity.");
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var keyProp = GetKeyProperty();
            if (keyProp == null)
                throw new InvalidOperationException($"No [Key] property found on {typeof(TEntity).Name}");

            // Optionally: Check if key is set
            var keyValue = keyProp.GetValue(entity);
            if (keyValue == null || (keyProp.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)keyValue)))
                throw new ArgumentException($"Key property '{keyProp.Name}' must be set before updating entity.");

            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
            var keyProp = GetKeyProperty();
            if (keyProp == null)
                throw new InvalidOperationException($"No [Key] property found on {typeof(TEntity).Name}");

            foreach (var entity in entities)
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                var keyValue = keyProp.GetValue(entity);
                if (keyValue == null || (keyProp.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)keyValue)))
                    throw new ArgumentException($"Key property '{keyProp.Name}' must be set before updating entity.");
            }
            _dbSet.UpdateRange(entities);
        }

        public async Task SoftDeleteAsync(string id)
        {
            var keyProp = GetKeyProperty();
            if (keyProp == null)
                throw new InvalidOperationException($"No [Key] property found on {typeof(TEntity).Name}");

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, keyProp);
            var idValue = Convert.ChangeType(id, keyProp.PropertyType);
            var equals = Expression.Equal(property, Expression.Constant(idValue));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            var entity = await _dbSet.FirstOrDefaultAsync(lambda);
            if (entity == null)
                throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with key {id} not found.");
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var keyProp = GetKeyProperty();
            if (keyProp == null)
                throw new InvalidOperationException($"No [Key] property found on {typeof(TEntity).Name}");

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, keyProp);
            var idValue = Convert.ChangeType(id, keyProp.PropertyType);
            var equals = Expression.Equal(property, Expression.Constant(idValue));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return await _dbSet.AnyAsync(lambda);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
