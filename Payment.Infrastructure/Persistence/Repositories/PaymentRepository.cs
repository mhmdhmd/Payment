using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Payment.Application.Interfaces.Repositories;
using Payment.Domain.DomainModels.Payment;
using Payment.Infrastructure.Persistence.DbContext;

namespace Payment.Infrastructure.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly MyDbContext _dbContext;

    public PaymentRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PaymentHistoryEntity>> GetAllAsync(params Expression<Func<PaymentHistoryEntity, object>>[] includeProperties)
    {
        IQueryable<PaymentHistoryEntity> query = _dbContext.Set<PaymentHistoryEntity>();

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }

    public async Task<PaymentHistoryEntity> GetByIdAsync(int id)
    {
        return await _dbContext.PaymentHistories.FindAsync(id);
    }

    public async Task AddAsync(PaymentHistoryEntity entity)
    {
        await _dbContext.PaymentHistories.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(PaymentHistoryEntity entity)
    {
        _dbContext.PaymentHistories.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(PaymentHistoryEntity entity)
    {
        _dbContext.PaymentHistories.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}