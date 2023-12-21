using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloCodeaholics.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null, int pageNumber = 0, int pageSize = 0, bool asNoTracking = true);
        Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task<T?> UpdateAsync(T entity, params Expression<Func<T, object>>[] excludedProperties);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    }
}
