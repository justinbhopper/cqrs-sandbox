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