using Microsoft.EntityFrameworkCore;
using UserCurrency.Common.Extensions;
using UserService.Api.ExceptionHandler;
using UserService.Application.Handlers;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Database;
using UserService.Infrastructure.Services;

namespace UserService.Api
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

			builder.Services.AddDbContext<UserDbContext>(options =>
			{
				var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

				options.UseNpgsql(
					connectionString,
					npgsql =>
					{
						npgsql.MigrationsAssembly("UserService.Infrastructure");
						npgsql.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null);
					}
				);

				options.EnableSensitiveDataLogging(false);
			});

			builder.Services.AddTransient<IUserRepository, UserRepository>();

			builder.Services.AddTransient<IRegistrationUserHandler, RegistrationUserHandler>();
			builder.Services.AddTransient<ILoginHandler, LoginHandler>();
			builder.Services.AddTransient<IHashPasswordService, HashPasswordService>();
			builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

			var app = builder.Build();

			app.UseExceptionHandler();

			app.MapControllers();

			app.Run();
		}
	}
}
