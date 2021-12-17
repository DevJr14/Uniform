using Domain.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<T, in TId> where T : class, IBaseEntity<TId>
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(TId id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        Task<T> AddRangeAsync(List<T> entities);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(List<T> entities);

        Task MarkDeletedAsync(T entity);

        Task MarkDeletedRangeAsync(List<T> entities);
    }
}
