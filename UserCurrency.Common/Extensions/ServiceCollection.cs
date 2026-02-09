using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserCurrency.Common.Options;

namespace UserCurrency.Common.Extensions
{
	public static class ServiceCollection
	{
		public static IServiceCollection AddJwtAuthorization(this IServiceCollection serviceDescriptors, IConfiguration configuration)
		{
			serviceDescriptors.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));

			var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>()
				?? throw new ApplicationException("AuthOptions is missing");

			serviceDescriptors.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer((options) => {
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = authOptions.Issuer,
						ValidateAudience = true,
						ValidAudience = authOptions.Audience,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),
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

			return serviceDescriptors;
		}
	}
}
