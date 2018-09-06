using System;
using System.ComponentModel.DataAnnotations;

namespace Sandbox.Domain.Models
{
	public class Film
	{
		[Range(0, long.MaxValue)]
		public Guid Id { get; set; }

		[Required]
		[StringLength(300, MinimumLength = 1)]
		public string Title { get; set; }

		[Range(1888, 2100)]
		public int Year { get; set; }

		[Range(1, int.MaxValue)]
		public int Rank { get; set; }
	}
}