using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public class ValidationQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
		where TQuery : IQuery<TResult>
	{
		private readonly IQueryHandler<TQuery, TResult> _inner;

		public ValidationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> inner)
		{
			_inner = inner;
		}

		public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default(CancellationToken))
		{
			var validationContext = new ValidationContext(query, null, null);
			Validator.ValidateObject(query, validationContext, true);

			return _inner.HandleAsync(query, cancellationToken);
		}
	}
}