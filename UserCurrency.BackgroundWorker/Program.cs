using CurrencyService.Application.Interfaces;
using CurrencyService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Text;
using UserCurrency.BackgroundWorker.BackgroundServices;
using UserCurrency.BackgroundWorker.Options;

namespace UserCurrency.BackgroundWorker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.Configure<CurrencyHostedServiceOptions>(builder.Configuration.GetSection("CurrencyHostedServiceOptions"));

			builder.Services.AddDbContext<CurrencyDbContext>(options =>
			{
				var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

				options.UseNpgsql(
					connectionString,
					npgsql =>
					{
						npgsql.MigrationsAssembly("CurrencyService.Infrastructure");
						npgsql.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null);
					}
				);

				options.EnableSensitiveDataLogging(false);
			});

			builder.Services.AddTransient<ICurrencyRepository, CurrencyRepository>();

			builder.Services.AddHostedService<CurrencyUpdaterHostedService>();

			builder.Services.AddHttpClient(
				"DailyCurrencyClient",
				client =>
				{
					client.BaseAddress = new Uri("https://www.cbr.ru");
				}
			);

			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			var app = builder.Build();

			app.Run();
		}
	}
}
