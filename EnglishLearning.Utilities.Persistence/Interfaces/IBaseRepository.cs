using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishLearning.Utilities.Persistence.Interfaces
{
    public interface IBaseRepository<T, TId> where T: class, IEntity<TId>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> filter);
        Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T item);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> filter);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAllAsync();
    }
}
