using CurrencyService.Api.ExceptionHandler;
using CurrencyService.Application.Handlers;
using CurrencyService.Application.Interfaces;
using CurrencyService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using UserCurrency.Common.Extensions;

namespace CurrencyService.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

			builder.Services.AddJwtAuthorization(builder.Configuration);

			builder.Services.AddProblemDetails();

			builder.Services.AddDbContext<CurrencyDbContext>(options =>
			{
				options.UseNpgsql(
					builder.Configuration.GetConnectionString("DefaultConnection"),
					npgsql =>
					{
						npgsql.MigrationsAssembly("CurrencyService.Infrastructure");
						npgsql.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null);
					}
				);

				options.EnableSensitiveDataLogging(false);
			});

			builder.Services.AddTransient<ICurrencyUserHandler, CurrencyUserHandler>();
			builder.Services.AddTransient<ICurrencyUserRepository, CurrencyUserRepository>();

			var app = builder.Build();

			app.UseExceptionHandler();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
