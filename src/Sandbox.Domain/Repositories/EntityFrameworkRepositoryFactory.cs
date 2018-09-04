using Microsoft.EntityFrameworkCore;

namespace Sandbox.Domain
{
	// Note: this should go under Sandbox.Infra.Data
	public class EntityFrameworkRepositoryFactory : IRepositoryFactory
	{
		private readonly DbContext _context;

		public EntityFrameworkRepositoryFactory(DbContext context)
		{
			_context = context;
		}

		public IRepository<TEntity> Create<TEntity>() where TEntity : class
		{
			return new EntityFrameworkRepository<TEntity>(_context);
		}
	}
}