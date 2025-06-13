using System.Linq.Expressions;
using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Interfaces;

public interface ICrudRepository<TEntity> where TEntity : BaseEntity
{
    Task<Guid> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
}