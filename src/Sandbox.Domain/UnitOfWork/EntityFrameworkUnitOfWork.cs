using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.Domain
{
	public class EntityFrameworkUnitOfWork : IUnitOfWork
	{
		private readonly DbContext _dbContext;
		private readonly IRepositoryFactory _repositoryFactory;

		public EntityFrameworkUnitOfWork(DbContext dbContext, IRepositoryFactory repositoryFactory)
		{
			_dbContext = dbContext;
			_repositoryFactory = repositoryFactory;
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			return _repositoryFactory.Create<TEntity>();
		}

		public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}