using System;

namespace Sandbox.Domain
{
	public class UpdateFilmCommand : CreateFilmCommand
	{
		public Guid Id { get; set; }
	}
}