using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserCurrency.Common.Exceptions;
using UserCurrency.Common.Options;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Services
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly IOptions<AuthOptions> authOptions;

		public JwtTokenService(IOptions<AuthOptions> authOptions)
		{
			this.authOptions = authOptions;
		}

		public string GetJwtToken(long userId)
		{
			var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
				?? throw new ApplicationErrorException("Jwt issuer env is empty");

			var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
				?? throw new ApplicationErrorException("Jwt audience env is empty");

			var key = Environment.GetEnvironmentVariable("JWT_KEY")
				?? throw new ApplicationErrorException("Jwt key env is empty");

			try
			{
				var jwtToken = new JwtSecurityToken(
					issuer,
					audience,
					[
						new (ClaimTypes.NameIdentifier, userId.ToString()),
					],
					expires: DateTime.UtcNow.AddMinutes(authOptions.Value.ExpirationTimeMin),
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
				);

				var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

				return token;
			}
			catch (Exception e)
			{
				throw new ApplicationErrorException("Error while create jwt token", e);
			}
		}
	}
}
