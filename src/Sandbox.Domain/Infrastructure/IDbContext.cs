using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public interface IDbContext : IQueryEntities, IUnitOfWork
	{
		Task<TEntity> GetAsync<TEntity>(Guid id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;
		IQueryable<TEntity> FindMany<TEntity>() where TEntity : class;
		void Add<TEntity>(TEntity entity) where TEntity : class;
		void Update<TEntity>(TEntity entity) where TEntity : class;
		void Remove<TEntity>(TEntity entity) where TEntity : class;
		Task ReloadAsync<TEntity>(TEntity entity) where TEntity : class;
	}
}