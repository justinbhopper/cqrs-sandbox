using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Domain;
using Sandbox.Domain.Models;

namespace Sandbox.Controllers
{
	[ApiController]
	[Route("api/v1/films")]
	public class FilmsController : ControllerBase
	{
		private readonly IQueryHandler<FilmQueries.GetAll, IList<Film>> _getAllFilms;
		private readonly IQueryHandler<FilmQueries.GetById, Film> _getFilm;
		private readonly ICommandHandler<FilmCommands.Create> _createFilm;
		private readonly ICommandHandler<FilmCommands.Update> _updateFilm;
		private readonly ICommandHandler<FilmCommands.Delete> _deleteFilm;

		public FilmsController(
			IQueryHandler<FilmQueries.GetAll, IList<Film>> getAllFilms,
			IQueryHandler<FilmQueries.GetById, Film> getfilm,
			ICommandHandler<FilmCommands.Create> createFilm,
			ICommandHandler<FilmCommands.Update> updateFilm,
			ICommandHandler<FilmCommands.Delete> deleteFilm)
		{
			_getAllFilms = getAllFilms;
			_getFilm = getfilm;
			_createFilm = createFilm;
			_updateFilm = updateFilm;
			_deleteFilm = deleteFilm;
		}

		// GET api/v1/films
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IList<Film>))]
		public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
		{
			return Ok(await _getAllFilms.HandleAsync(new FilmQueries.GetAll(), cancellationToken));
		}

		// GET api/v1/films/5
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Film))]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetByIdAsync(FilmQueries.GetById command, CancellationToken cancellationToken)
		{
			var film = await _getFilm.HandleAsync(command, cancellationToken);

			if (film == null)
				return NotFound();
			return Ok(film);
		}

		// POST api/v1/films/create
		[HttpPost("create")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> CreateAsync([FromBody] FilmCommands.Create command, CancellationToken cancellationToken)
		{
			await _createFilm.HandleAsync(command, cancellationToken);
			return NoContent();
		}

		// POST api/v1/films/update
		[HttpPost("update")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> UpdateAsync([FromBody] FilmCommands.Update command, CancellationToken cancellationToken)
		{
			await _updateFilm.HandleAsync(command, cancellationToken);
			return NoContent();
		}

		// POST api/v1/films/delete
		[HttpPost("delete")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> DeleteAsync([FromBody] FilmCommands.Delete command, CancellationToken cancellationToken)
		{
			await _deleteFilm.HandleAsync(command, cancellationToken);
			return NoContent();
		}
	}
}
