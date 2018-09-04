using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public interface IUnitOfWork
	{
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
		Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}