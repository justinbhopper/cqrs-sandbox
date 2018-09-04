using System.Linq;

namespace Sandbox.Domain
{
	public interface IQueryEntities
	{
		IQueryable<TEntity> Query<TEntity>() where TEntity : class;
	}
}