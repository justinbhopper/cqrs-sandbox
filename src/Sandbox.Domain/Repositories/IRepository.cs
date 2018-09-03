using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Domain
{
	public interface IRepository<TEntity>
	{
		Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
		IQueryable<TEntity> AsQueryable();
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Remove(TEntity entity);
	}
}