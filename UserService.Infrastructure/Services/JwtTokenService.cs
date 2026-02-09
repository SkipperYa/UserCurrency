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
			try
			{
				var jwtToken = new JwtSecurityToken(
					authOptions.Value.Issuer,
					authOptions.Value.Audience,
					[
						new (ClaimTypes.NameIdentifier, userId.ToString()),
					],
					expires: DateTime.UtcNow.AddDays(30),
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Value.Key)), SecurityAlgorithms.HmacSha256)
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
