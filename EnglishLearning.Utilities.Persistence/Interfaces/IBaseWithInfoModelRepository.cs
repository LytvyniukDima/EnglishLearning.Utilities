using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishLearning.Utilities.Persistence.Interfaces
{
    public interface IBaseWithInfoModelRepository<T, TInfo> where T : class where TInfo : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T item);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> filter);
        Task<bool> UpdateAsync(string id, T item);
        Task<bool> DeleteAllAsync();
        
        Task<IEnumerable<TInfo>> GetAllInfoAsync();
        Task<IEnumerable<TInfo>> FindAllInfoAsync(Expression<Func<T, bool>> filter);
    }
}
