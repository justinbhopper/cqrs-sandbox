using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public class DeadlockRetryCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
	{
		private readonly ICommandHandler<TCommand> _inner;

		public DeadlockRetryCommandHandlerDecorator(ICommandHandler<TCommand> inner)
		{
			_inner = inner;
		}

		public Task HandleAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
		{
			const int RETRIES = 5;
			return HandleWithRetryAsync(command, RETRIES, cancellationToken);
		}

		private async Task HandleWithRetryAsync(TCommand command, int retries, CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				await _inner.HandleAsync(command, cancellationToken);
			}
			catch (Exception ex) when (IsDeadlockException(ex))
			{
				if (retries <= 0)
					throw;

				await Task.Delay(300, cancellationToken);

				await HandleWithRetryAsync(command, retries - 1, cancellationToken);
			}
		}

		private static bool IsDeadlockException(Exception ex)
		{
			while (ex != null)
			{
				if (ex.Message.Contains("deadlock"))
					return true;

				ex = ex.InnerException;
			}

			return false;
		}
	}
}