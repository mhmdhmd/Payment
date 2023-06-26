using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Payment.Application.Interfaces.Repositories;
using Payment.Domain.DomainModels;
using Payment.Infrastructure.Persistence.DbContext;

namespace Payment.Infrastructure.Persistence.Repositories;

public abstract class GenericRepository<TEntity, TKey> : IBaseRepository<TEntity,TKey> where TEntity : EntityBase<TKey>
{
    private readonly IMyDbContext _dbContext;
    private DbSet<TEntity> Set;

    public GenericRepository(IMyDbContext dbContext)
    {
        _dbContext = dbContext;
        Set = dbContext.GetDbSet<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = Set.AsNoTracking();
        
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }
    
    public virtual async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await Set.FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await Set.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        Set.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        Set.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}