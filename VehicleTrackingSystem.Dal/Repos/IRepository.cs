using System.Linq.Expressions;

namespace VehicleTrackingSystem.Dal.Repos
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> All();
        Task<IQueryable<TEntity>> GetManyWhere(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> GetManyWhereWithIncludes(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleWhere(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetSingleWhereWithIncludes(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        Task AddAsync(TEntity entity);
        Task AddMultipleAsync(List<TEntity> entityList);
        void Delete(TEntity entity);
        void DeleteMultiple(List<TEntity> entityList);
        void Update(TEntity entity);
        Task<int> CommitAsync();
    }
}
