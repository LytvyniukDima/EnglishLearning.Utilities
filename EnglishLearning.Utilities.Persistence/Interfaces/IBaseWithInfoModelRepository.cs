using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishLearning.Utilities.Persistence.Interfaces
{
    public interface IBaseWithInfoModelRepository<T, TInfo, TId>: IBaseRepository<T, TId> where T : class, IEntity<TId> where TInfo : class
    {
        Task<IEnumerable<TInfo>> GetAllInfoAsync();
        Task<IEnumerable<TInfo>> FindAllInfoAsync(Expression<Func<T, bool>> filter);
    }
}
