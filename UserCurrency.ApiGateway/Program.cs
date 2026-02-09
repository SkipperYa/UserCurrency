using UserCurrency.Common.Extensions;

namespace UserCurrency.ApiGateway
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddReverseProxy()
				.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

			builder.Services.AddAuthorizationBuilder()
				.AddPolicy("authPolicy", policy => policy.RequireAuthenticatedUser());

			builder.Services.AddJwtAuthorization(builder.Configuration);

			var app = builder.Build();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapReverseProxy();

			app.Run();
		}
	}
}
