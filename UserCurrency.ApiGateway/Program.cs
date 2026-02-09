using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer((options) => {
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = builder.Configuration["AuthOptions:issuer"],
						ValidateAudience = true,
						ValidAudience = builder.Configuration["AuthOptions:audience"],
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthOptions:key"] ?? "")),
						ValidateIssuerSigningKey = true,
					};
#if DEBUG
					options.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = context =>
						{
							Console.WriteLine($"Authentication failed: {context.Exception.Message}");
							return Task.CompletedTask;
						},
						OnTokenValidated = context =>
						{
							Console.WriteLine("Token validated successfully.");
							return Task.CompletedTask;
						}
					};
#endif
				});

			var app = builder.Build();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapReverseProxy();

			app.Run();
		}
	}
}
