using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VehicleTrackingSystem.Dal.Context;

namespace VehicleTrackingSystem.Dal.Repos
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly PostgresDbContext _context;

        public BaseRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<IQueryable<TEntity>> GetManyWhere(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetManyWhereWithIncludes(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

            return query.Where(expression);
        }

        public async Task<TEntity> GetSingleWhere(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> GetSingleWhereWithIncludes(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

            return query.Where(expression).FirstOrDefault();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddMultipleAsync(List<TEntity> entityList)
        {
            await _context.Set<TEntity>().AddRangeAsync(entityList);
        }


        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteMultiple(List<TEntity> entityList)
        {
            _context.Set<TEntity>().RemoveRange(entityList);
        }

        public void Update(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void IntiailizeDate<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                _context.Entry<TEntity>(entity).Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                _context.Entry<TEntity>(entity).Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
            }
        }

        public void IntiailizeDate<TEntity>(List<TEntity> entityList) where TEntity : class
        {
            if (entityList != null && entityList.Count > 0)
            {
                foreach (TEntity entity in entityList)
                {
                    _context.Entry<TEntity>(entity).Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                    _context.Entry<TEntity>(entity).Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}
