using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public class ValidationQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
		where TQuery : IQuery<TResult>
	{
		private readonly IQueryHandler<TQuery, TResult> _queryHandler;

		public ValidationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> queryHandler)
		{
			_queryHandler = queryHandler;
		}

		public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default(CancellationToken))
		{
			var validationContext = new ValidationContext(query, null, null);
			Validator.ValidateObject(query, validationContext, validateAllProperties: true);

			return _queryHandler.HandleAsync(query, cancellationToken);
		}
	}
}