namespace Sandbox.Domain
{
	public interface IRepositoryFactory
	{
		IRepository<TEntity> Create<TEntity>() where TEntity : class;
	}
}