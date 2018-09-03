using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sandbox.Domain;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	// Note: this should go under Sandbox.Infra.Data
	public class EntityFrameworkRepository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		private readonly DbSet<TEntity> _dbSet;

		public EntityFrameworkRepository(DbContext context)
		{
			_dbSet = context.Set<TEntity>();
		}

		public virtual Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
		{
			return _dbSet.FindAsync(id, cancellationToken);
		}

		public virtual IQueryable<TEntity> AsQueryable()
		{
			return _dbSet.AsNoTracking();
		}

		public virtual void Add(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public virtual void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		public virtual void Remove(TEntity entity)
		{
			_dbSet.Remove(entity);
		}
	}
}