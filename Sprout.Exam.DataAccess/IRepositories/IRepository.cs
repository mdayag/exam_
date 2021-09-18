using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.IRepositories
{
    public interface IRepository : IReadOnlyRepository
    {
        DatabaseFacade Database { get; }
        void Save();
        Task SaveAsync();
        void Create<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void BulkCreate<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Delete<TEntity>(object entity) where TEntity : class, IEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
    }
}
