using Domain.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IBaseRepository<T, TId> RepositoryFor<T>() where T : AuditableEntity<TId>;

        Task<int> Commit(CancellationToken cancellationToken);

        Task Rollback();
    }
}
