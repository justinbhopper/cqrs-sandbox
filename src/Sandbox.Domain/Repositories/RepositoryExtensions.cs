using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public static class RepositoryExtensions
	{
		public static async Task RemoveAsync<TEntity>(this IRepository<TEntity> repository, Guid id, CancellationToken cancellationToken = default(CancellationToken))
		{
			var entity = await repository.GetAsync(id, cancellationToken);
			repository.Remove(entity);
		}
	}
}