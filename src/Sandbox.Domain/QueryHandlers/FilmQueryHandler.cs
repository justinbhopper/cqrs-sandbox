using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class FilmQueryHandler : 
		IQueryHandler<FilmQueries.GetAll, IList<Film>>,
		IQueryHandler<FilmQueries.GetById, Film>,
		IQueryHandler<FilmQueries.SearchByTitle, IList<Film>> 
	{
		private readonly IRepository<Film> _repository;

		public FilmQueryHandler(IRepository<Film> repository)
		{
			_repository = repository;
		}

		public async Task<IList<Film>> HandleAsync(FilmQueries.GetAll query, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await OrderBy(_repository.AsQueryable(), query.SortBy).ToListAsync();
		}

		public async Task<Film> HandleAsync(FilmQueries.GetById query, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await _repository.GetAsync(query.Id);
		}

		public async Task<IList<Film>> HandleAsync(FilmQueries.SearchByTitle query, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await _repository.AsQueryable()
				.Where(f => StringComparer.Compare(f.Title, query.Title, query.TitleComparison))
				.ToListAsync();
		}

		private IQueryable<Film> OrderBy(IQueryable<Film> query, FilmQueries.GetAllSortField sortBy)
		{
			switch (sortBy)
			{
				case FilmQueries.GetAllSortField.Rank:
					return query.OrderBy(f => f.Rank);
				case FilmQueries.GetAllSortField.Title:
					return query.OrderBy(f => f.Title);
				default:
					throw new ArgumentOutOfRangeException(nameof(sortBy));
			}
		}
	}
}