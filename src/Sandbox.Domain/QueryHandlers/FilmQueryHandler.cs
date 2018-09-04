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
		IQueryHandler<GetAllFilmsQuery, IList<Film>>,
		IQueryHandler<GetFilmQuery, Film>,
		IQueryHandler<SearchFilmsByTitleQuery, IList<Film>> 
	{
		private readonly IRepository<Film> _repository;

		public FilmQueryHandler(IRepository<Film> repository)
		{
			_repository = repository;
		}

		public async Task<IList<Film>> HandleAsync(GetAllFilmsQuery query, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await OrderBy(_repository.AsQueryable(), query.SortBy).ToListAsync(cancellationToken);
		}

		public async Task<Film> HandleAsync(GetFilmQuery query, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await _repository.GetAsync(query.Id, cancellationToken);
		}

		public async Task<IList<Film>> HandleAsync(SearchFilmsByTitleQuery query, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await _repository.AsQueryable()
				.Where(f => StringComparer.Compare(f.Title, query.Title, query.TitleComparison))
				.ToListAsync(cancellationToken);
		}

		private IQueryable<Film> OrderBy(IQueryable<Film> query, GetAllFilmsSortField sortBy)
		{
			switch (sortBy)
			{
				case GetAllFilmsSortField.Rank:
					return query.OrderBy(f => f.Rank);
				case GetAllFilmsSortField.Title:
					return query.OrderBy(f => f.Title);
				default:
					throw new ArgumentOutOfRangeException(nameof(sortBy));
			}
		}
	}
}