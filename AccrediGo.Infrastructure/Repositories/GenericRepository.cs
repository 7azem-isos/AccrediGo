using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccrediGo.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly DbSet<T> _entities;
        private readonly AccrediGoDbContext _context;
        private readonly IMapper _mapper;
        private bool _isDisposed;

        public GenericRepository(AccrediGoDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entities = _context.Set<T>();
            _isDisposed = false;
        }

        #region Query Operations

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
                return await _entities.CountAsync(cancellationToken);
            
            return await _entities.CountAsync(predicate, cancellationToken);
        }

        #endregion

        #region Command Operations

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entry = await _entities.AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var entityList = entities.ToList();
            await _entities.AddRangeAsync(entityList, cancellationToken);
            return entityList;
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _entities.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _entities.RemoveRange(entities);
        }

        #endregion

        #region Pagination

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber, 
            int pageSize, 
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default)
        {
            var query = _entities.AsQueryable();

            // Apply predicate if provided
            if (predicate != null)
                query = query.Where(predicate);

            // Get total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply ordering if provided
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            // Apply pagination
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        #endregion

        #region Include Related Entities

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            var query = _entities.AsQueryable();
            
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                _context?.Dispose();
            }
            _isDisposed = true;
        }

        #endregion
    }
} 