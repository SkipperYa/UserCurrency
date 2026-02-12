using CurrencyService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Infrastructure.Database;

namespace UserCurrency.MigrationService
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			try
			{
				var builder = Host.CreateDefaultBuilder(args)
					.ConfigureServices((context, services) =>
					{
						var connection = Environment.GetEnvironmentVariable("CONNECTION_STRING");

						services.AddDbContext<UserDbContext>(options => options.UseNpgsql(connection));

						services.AddDbContext<CurrencyDbContext>(options => options.UseNpgsql(connection));
					});

				var host = builder.Build();

				using (var scope = host.Services.CreateScope())
				{
					var serviceProvider = scope.ServiceProvider;

					await serviceProvider.GetRequiredService<UserDbContext>().Database.MigrateAsync();

					await serviceProvider.GetRequiredService<CurrencyDbContext>().Database.MigrateAsync();
				}

				Console.WriteLine("Migrations done.");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
