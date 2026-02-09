using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Database
{
	public class ApplicationContext : DbContext
	{
		public DbSet<User> Users => Set<User>();

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
		}
	}
}
