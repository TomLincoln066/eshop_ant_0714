using System.Linq.Expressions;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly PromotionDbContext _dbContext;

    public BaseRepository(PromotionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity =  await _dbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return 0;
        }

        _dbContext.Set<T>().Remove(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public Task<List<T>> GetAllAsync()
    {
        return _dbContext.Set<T>().ToListAsync();
    }

    public Task<T> GetByIdAsync(int id)
    {
        return _dbContext.Set<T>().FindAsync(id).AsTask();
    }

    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public Task<int> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbContext.Set<T>().Where(filter).ToListAsync();
    }
}