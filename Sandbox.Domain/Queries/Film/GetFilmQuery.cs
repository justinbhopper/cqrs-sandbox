using System;
using System.ComponentModel.DataAnnotations;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class GetFilmQuery : IQuery<Film>
	{
		public GetFilmQuery() { }

		public GetFilmQuery(Guid id)
		{
			Id = id;
		}

		[Range(1, long.MaxValue)]
		public Guid Id { get; set; }
	}
}