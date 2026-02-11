using CurrencyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyService.Infrastructure.Database
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Currency> Currencies => Set<Currency>();
		public DbSet<CurrencyUser> UserCurrencies => base.Set<CurrencyUser>();

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
		}
	}
}
