using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.Domain
{
	// Note: this should go under Sandbox.Infra.Data
	public class EntityFrameworkRepository : IRepository
	{
		private readonly DbContext _context;

		public EntityFrameworkRepository(DbContext context)
		{
			_context = context;
		}

		public virtual Task<TEntity> GetAsync<TEntity>(Guid id, CancellationToken cancellationToken = default(CancellationToken))
			where TEntity : class
		{
			return _context.Set<TEntity>().FindAsync(id, cancellationToken);
		}

		public virtual IQueryable<TEntity> Query<TEntity>()
			where TEntity : class
		{
			return _context.Set<TEntity>().AsNoTracking();
		}

		public virtual IQueryable<TEntity> FindMany<TEntity>()
			where TEntity : class
		{
			return _context.Set<TEntity>();
		}

		public virtual void Add<TEntity>(TEntity entity)
			where TEntity : class
		{
			_context.Set<TEntity>().Add(entity);
		}

		public virtual void Update<TEntity>(TEntity entity)
			where TEntity : class
		{
			_context.Set<TEntity>().Update(entity);
		}

		public virtual void Remove<TEntity>(TEntity entity)
			where TEntity : class
		{
			_context.Set<TEntity>().Remove(entity);
		}

		public virtual Task ReloadAsync<TEntity>(TEntity entity)
			where TEntity : class
		{
			return _context.Entry(entity).ReloadAsync();
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}