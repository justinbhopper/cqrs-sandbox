using System.Collections.Generic;
using System.ComponentModel;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public enum GetAllFilmsSortField
	{
		Rank,
		Title
	}

	public class GetAllFilmsQuery : IQuery<IList<Film>>
	{
		[DefaultValue(GetAllFilmsSortField.Rank)]
		public GetAllFilmsSortField SortBy { get; set; } = GetAllFilmsSortField.Rank;
		
		[DefaultValue(0)]
		public int StartIndex { get; set; } = 0;

		public int? PageSize { get; set; }
	}
}