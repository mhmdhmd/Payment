using System.Linq.Expressions;
using Payment.Domain.DomainModels;

namespace Payment.Application.Interfaces.Repositories;

/// <summary>
/// Represents the base repository interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
public interface IBaseRepository<TEntity, in TKey> where TEntity: EntityBase<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}