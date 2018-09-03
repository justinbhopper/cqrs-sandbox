using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public interface ICommandHandler<TCommand>
	{
		Task HandleAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken));
	}
}