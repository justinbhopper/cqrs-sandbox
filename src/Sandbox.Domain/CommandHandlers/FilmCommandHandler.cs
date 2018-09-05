using System.Threading;
using System.Threading.Tasks;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class FilmCommandHandler : 
		ICommandHandler<CreateFilmCommand>,
		ICommandHandler<UpdateFilmCommand>,
		ICommandHandler<DeleteFilmCommand> 
	{
		private readonly IDbContext _context;

		public FilmCommandHandler(IDbContext context)
		{
			_context = context;
		}

		public async Task HandleAsync(CreateFilmCommand command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = new Film
			{
				Title = command.Title,
				Year = command.Year,
				Rank = command.Rank
			};

			_context.Add(film);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task HandleAsync(UpdateFilmCommand command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = await _context.GetAsync<Film>(command.Id, cancellationToken)
				?? throw new ResourceNotFoundException(command.Id, nameof(Film));

			film.Title = command.Title;
			film.Year = command.Year;
			film.Rank = command.Rank;

			_context.Update(film);

			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task HandleAsync(DeleteFilmCommand command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = await _context.GetAsync<Film>(command.FilmId, cancellationToken)
				?? throw new ResourceNotFoundException(command.FilmId, nameof(Film));

			_context.Remove(film);

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}