using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
	{
		private readonly ICommandHandler<TCommand> _inner;

		public ValidationCommandHandlerDecorator(ICommandHandler<TCommand> inner)
		{
			_inner = inner;
		}

		public Task HandleAsync(TCommand query, CancellationToken cancellationToken = default(CancellationToken))
		{
			var validationContext = new ValidationContext(query, null, null);
			Validator.ValidateObject(query, validationContext, true);

			return _inner.HandleAsync(query, cancellationToken);
		}
	}
}