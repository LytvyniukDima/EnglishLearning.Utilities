using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishLearning.Utilities.Persistence.Interfaces
{
    public interface IBaseRepository<T, TId> 
        where T : class, IEntity<TId>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> filter);
        Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T item);
        Task<IReadOnlyList<T>> AddManyAsync(IReadOnlyList<T> items);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> filter);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAllAsync();
    }
}
