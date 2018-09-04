using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public interface ICommandHandler<in TCommand>
	{
		Task HandleAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken));
	}
}