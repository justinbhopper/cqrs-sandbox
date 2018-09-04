using System.ComponentModel.DataAnnotations;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class CreateFilmCommand
	{
		[Required]
		[StringLength(300, MinimumLength = 1)]
		public string Title { get; set; }

		[Range(1888, 2100)]
		public int Year { get; set; }

		[Range(1, int.MaxValue)]
		public int Rank { get; set; }
	}
}