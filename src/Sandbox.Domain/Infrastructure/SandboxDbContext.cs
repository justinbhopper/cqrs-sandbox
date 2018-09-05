using Microsoft.EntityFrameworkCore;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class SandboxDbContext : EntityFrameworkDbContext
	{
		public SandboxDbContext(DbContextOptions contextOptions)
			: base(contextOptions) { }

		public DbSet<Film> Films { get; set; }
	}
}