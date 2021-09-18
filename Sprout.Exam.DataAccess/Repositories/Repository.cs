using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Sprout.Exam.DataAccess.IRepositories;
using Sprout.Exam.WebApp.DataAccess;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class Repository<TContext> : ReadOnlyRepository<TContext>, IRepository
    where TContext : ApplicationDbContext
    {
        public Repository(TContext context) : base(context)
        {

        }

        public DatabaseFacade Database => _context.Database;

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        void IRepository.Create<TEntity>(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        void IRepository.BulkCreate<TEntity>(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        void IRepository.Delete<TEntity>(object id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        void IRepository.Update<TEntity>(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
