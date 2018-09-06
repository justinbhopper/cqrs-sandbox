using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.Domain
{
	public abstract class EntityFrameworkDbContext : DbContext, IDbContext
	{
		protected EntityFrameworkDbContext(DbContextOptions contextOptions)
			: base(contextOptions) { }

		Task<TEntity> IDbContext.GetAsync<TEntity>(Guid id, CancellationToken cancellationToken)
		{
			return Set<TEntity>().FindAsync(id, cancellationToken);
		}

		IQueryable<TEntity> IQueryEntities.Query<TEntity>()
		{
			return Set<TEntity>().AsNoTracking();
		}

		IQueryable<TEntity> IDbContext.FindMany<TEntity>()
		{
			return Set<TEntity>();
		}

		void IDbContext.Add<TEntity>(TEntity entity)
		{
			Add(entity);
		}

		void IDbContext.Update<TEntity>(TEntity entity)
		{
			Update(entity);
		}

		void IDbContext.Remove<TEntity>(TEntity entity)
		{
			Remove(entity);
		}

		Task IDbContext.ReloadAsync<TEntity>(TEntity entity)
		{
			return Entry(entity).ReloadAsync();
		}

		async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
		{
			await SaveChangesAsync(cancellationToken);
		}
	}
}