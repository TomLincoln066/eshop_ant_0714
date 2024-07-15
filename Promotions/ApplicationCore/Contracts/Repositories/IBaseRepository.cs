using System.Linq.Expressions;

namespace ApplicationCore.Contracts.Repositories;

public interface IBaseRepository<T> where T:class
{
    Task<int> DeleteAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> filter);
}