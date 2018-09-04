using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class SearchFilmsByTitleQuery : IQuery<IList<Film>>
	{
		[Required, StringLength(300, MinimumLength = 1)]
		public string Title { get; set; }

		public StringRelation TitleComparison { get; set; } = StringRelation.StartsWith;
	}
}