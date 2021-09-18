using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.IRepositories;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class ReadOnlyRepository<TContext> : IReadOnlyRepository
    where TContext : DbContext
    {
        protected TContext _context;

        public ReadOnlyRepository(TContext context) => _context = context;

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, int? skip, int? take, bool? includeDeleted)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(null, order, includeproperties, skip, take, includeDeleted).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsyncVM<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, int? skip, int? take, bool? includeDeleted)
        where TEntity : class, IEntityVM
        {
            return await GetQueryableVM<TEntity>(null, order, includeproperties, skip, take, includeDeleted).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsyncVM<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, int? skip, int? take, bool? includeDeleted)
        where TEntity : class, IEntityVM
        {
            return await GetQueryableVM<TEntity>(filter, order, includeproperties, skip, take, includeDeleted).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsyncTypeVM<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, int? skip, int? take, bool? includeDeleted)
        where TEntity : class, IEntityTypeVM
        {
            return await GetQueryableTypeVM<TEntity>(null, order, includeproperties, skip, take, includeDeleted).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> filter, string includeproperties, bool? includeDeleted)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, null, includeproperties, null, null, includeDeleted).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id)
        where TEntity : class, IEntity
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetFirst<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, bool? includeDeleted)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, order, includeproperties, null, null, includeDeleted).FirstOrDefaultAsync();
        }

        public TEntity GetFirstNonAsync<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, bool? includeDeleted)
        where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, order, includeproperties, null, null, includeDeleted).FirstOrDefault();
        }

        public async Task<TEntity> GetFirstVM<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, bool? includeDeleted)
        where TEntity : class, IEntityVM
        {
            return await GetQueryableVM<TEntity>(filter, order, includeproperties, null, null, includeDeleted).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstTypeVM<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, string includeproperties, bool? includeDeleted)
        where TEntity : class, IEntityTypeVM
        {
            return await GetQueryableTypeVM<TEntity>(filter, order, includeproperties, null, null, includeDeleted).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> Query<TEntity>()
        where TEntity : class, IEntity
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> VMQuery<TEntity>()
        where TEntity : class, IEntityVM
        {
            return _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeproperties = null,
            int? skip = null,
            int? take = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntity
        {
            includeproperties = includeproperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeproperty in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeproperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (includeDeleted == null || includeDeleted.HasValue && !includeDeleted.Value)
                query = query.Where(m => m.IsDeleted == false);

            return query;
        }

        public virtual IQueryable<TEntity> GetQueryableVM<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeproperties = null,
            int? skip = null,
            int? take = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntityVM
        {
            includeproperties = includeproperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeproperty in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeproperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        public virtual IQueryable<TEntity> GetQueryableTypeVM<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeproperties = null,
            int? skip = null,
            int? take = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntityTypeVM
        {
            includeproperties = includeproperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeproperty in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeproperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetFilteredVM<TEntity>(Expression<Func<TEntity, bool>> filter, string includeproperties, bool? includeDeleted)
        where TEntity : class, IEntityVM
        {
            return await GetQueryableVM<TEntity>(filter, null, includeproperties, null, null, includeDeleted).ToListAsync();
        }
    }
}
