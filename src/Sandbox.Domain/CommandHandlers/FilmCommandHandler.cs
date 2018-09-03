using System;
using System.Threading;
using System.Threading.Tasks;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class FilmCommandHandler : 
		ICommandHandler<FilmCommands.Create>,
		ICommandHandler<FilmCommands.Update>,
		ICommandHandler<FilmCommands.Delete> 
	{
		private readonly IRepository<Film> _repository;
		private readonly IUnitOfWork _uow;

		public FilmCommandHandler(IUnitOfWork uow)
		{
			_uow = uow;
			_repository = uow.GetRepository<Film>();
		}

		public async Task HandleAsync(FilmCommands.Create command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = new Film
			{
				Title = command.Title,
				Year = command.Year,
				Rank = command.Rank
			};

			_repository.Add(film);
			await _uow.CommitAsync(cancellationToken);
		}

		public async Task HandleAsync(FilmCommands.Update command, CancellationToken cancellationToken = default(CancellationToken))
		{
			var film = new Film
			{
				Id = command.Id,
				Title = command.Title,
				Year = command.Year,
				Rank = command.Rank
			};

			_repository.Update(film);
			await _uow.CommitAsync(cancellationToken);
		}

		public async Task HandleAsync(FilmCommands.Delete command, CancellationToken cancellationToken = default(CancellationToken))
		{
			await _repository.RemoveAsync(command.FilmId, cancellationToken);
			await _uow.CommitAsync(cancellationToken);
		}
	}
}