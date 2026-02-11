using CurrencyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyService.Infrastructure.Database
{
	public class CurrencyDbContext : DbContext
	{
		public DbSet<Currency> Currencies => Set<Currency>();
		public DbSet<CurrencyUser> UserCurrencies => Set<CurrencyUser>();

		public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CurrencyDbContext).Assembly);
		}
	}
}
