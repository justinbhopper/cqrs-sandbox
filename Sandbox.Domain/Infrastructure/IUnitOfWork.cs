using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public interface IUnitOfWork
	{
		Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}