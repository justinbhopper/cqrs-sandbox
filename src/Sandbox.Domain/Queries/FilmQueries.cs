using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class FilmQueries
	{
		public class GetById : IQuery<Film>
		{
			public GetById() { }

			public GetById(Guid id)
			{
				Id = id;
			}

			[Range(1, long.MaxValue)]
			public Guid Id { get; set; }
		}

		public enum GetAllSortField
		{
			Rank,
			Title
		}

		public class GetAll : IQuery<IList<Film>>
		{
			[DefaultValue(GetAllSortField.Rank)]
			public GetAllSortField SortBy { get; set; } = GetAllSortField.Rank;
			
			[DefaultValue(0)]
			public int StartIndex { get; set; } = 0;

			public int? PageSize { get; set; }
		}

		public class SearchByTitle : IQuery<IList<Film>>
		{
			[Required, StringLength(300, MinimumLength = 1)]
			public string Title { get; set; }

			public StringRelation TitleComparison { get; set; } = StringRelation.StartsWith;
		}
	}
}