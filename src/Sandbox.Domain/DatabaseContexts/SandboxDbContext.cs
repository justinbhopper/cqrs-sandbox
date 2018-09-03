using Microsoft.EntityFrameworkCore;
using Sandbox.Domain.Models;

namespace Sandbox.Domain
{
	public class SandboxDbContext : DbContext
	{
		public SandboxDbContext(DbContextOptions options)
			: base(options) { }

		public DbSet<Film> Films { get; set; }
	}
}