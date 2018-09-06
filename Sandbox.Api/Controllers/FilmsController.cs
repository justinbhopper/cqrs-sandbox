using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Domain;
using Sandbox.Domain.Models;

namespace Sandbox.Controllers
{
	public class FilmsController : ApiController
	{
		private readonly IQueryHandler<GetAllFilmsQuery, IList<Film>> _getAllFilms;
		private readonly IQueryHandler<GetFilmQuery, Film> _getFilm;
		private readonly ICommandHandler<CreateFilmCommand> _createFilm;
		private readonly ICommandHandler<UpdateFilmCommand> _updateFilm;
		private readonly ICommandHandler<DeleteFilmCommand> _deleteFilm;

		public FilmsController(
			IQueryHandler<GetAllFilmsQuery, IList<Film>> getAllFilms,
			IQueryHandler<GetFilmQuery, Film> getfilm,
			ICommandHandler<CreateFilmCommand> createFilm,
			ICommandHandler<UpdateFilmCommand> updateFilm,
			ICommandHandler<DeleteFilmCommand> deleteFilm)
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
			return Ok(await _getAllFilms.HandleAsync(new GetAllFilmsQuery(), cancellationToken));
		}

		// GET api/v1/films/5
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Film))]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetByIdAsync(GetFilmQuery command, CancellationToken cancellationToken)
		{
			return Ok(await _getFilm.HandleAsync(command, cancellationToken));
		}

		// POST api/v1/films/create
		[HttpPost("create")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> CreateAsync([FromBody] CreateFilmCommand command, CancellationToken cancellationToken)
		{
			await _createFilm.HandleAsync(command, cancellationToken);
			return NoContent();
		}

		// POST api/v1/films/update
		[HttpPost("update")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> UpdateAsync([FromBody] UpdateFilmCommand command, CancellationToken cancellationToken)
		{
			await _updateFilm.HandleAsync(command, cancellationToken);
			return NoContent();
		}

		// POST api/v1/films/delete
		[HttpPost("delete")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> DeleteAsync([FromBody] DeleteFilmCommand command, CancellationToken cancellationToken)
		{
			await _deleteFilm.HandleAsync(command, cancellationToken);
			return NoContent();
		}
	}
}
