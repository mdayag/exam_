using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.IRepositories
{
    public interface IReadOnlyRepository
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
            string includeproperties = null,
            int? skip = null,
            int? take = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAllAsyncVM<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
            string includeproperties = null,
            int? skip = null,
            int? take = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntityVM;

        Task<IEnumerable<TEntity>> GetAllAsyncVM<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
            string includeproperties = null,
            int? skip = null,
            int? take = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntityVM;

        Task<IEnumerable<TEntity>> GetAllAsyncTypeVM<TEntity>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
        string includeproperties = null,
        int? skip = null,
        int? take = null,
        bool? includeDeleted = null
    ) where TEntity : class, IEntityTypeVM;
        Task<TEntity> GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
            string includeproperties = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntity;

        TEntity GetFirstNonAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
            string includeproperties = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntity;

        Task<TEntity> GetFirstVM<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
            string includeproperties = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntityVM;

        Task<TEntity> GetFirstTypeVM<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order,
            string includeproperties = null,
            bool? includeDeleted = null
        ) where TEntity : class, IEntityTypeVM;

        Task<IEnumerable<TEntity>> GetFiltered<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            string includeproperties,
            bool? includeDeleted = null)
            where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetFilteredVM<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            string includeproperties,
            bool? includeDeleted = null)
            where TEntity : class, IEntityVM;

        Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class, IEntity;
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
        IQueryable<TEntity> VMQuery<TEntity>() where TEntity : class, IEntityVM;
    }
}
