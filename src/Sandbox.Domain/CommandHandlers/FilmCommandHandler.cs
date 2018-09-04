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
		private readonly IRepository _repository;

		public FilmCommandHandler(IRepository repository)
		{
			_repository = repository;
		}

		public async Task HandleAsync(CreateFilmCommand command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = new Film
			{
				Title = command.Title,
				Year = command.Year,
				Rank = command.Rank
			};

			_repository.Add(film);

			await _repository.SaveChangesAsync(cancellationToken);
		}

		public async Task HandleAsync(UpdateFilmCommand command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = await _repository.GetAsync<Film>(command.Id, cancellationToken)
				?? throw new ResourceNotFoundException(command.Id, nameof(Film));

			film.Title = command.Title;
			film.Year = command.Year;
			film.Rank = command.Rank;

			_repository.Update(film);

			await _repository.SaveChangesAsync(cancellationToken);
		}

		public async Task HandleAsync(DeleteFilmCommand command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = await _repository.GetAsync<Film>(command.FilmId, cancellationToken)
				?? throw new ResourceNotFoundException(command.FilmId, nameof(Film));

			_repository.Remove(film);

			await _repository.SaveChangesAsync(cancellationToken);
		}
	}
}