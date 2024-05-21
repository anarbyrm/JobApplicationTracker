using JobApplicationTracker.Core.Entities;
using System.Linq.Expressions;

namespace JobApplicationTracker.DataAccess.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int limit, int offset);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
