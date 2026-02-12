using System.Security.Cryptography;
using System.Text;
using UserCurrency.Common.Exceptions;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Services
{
	public class HashPasswordService : IHashPasswordService
	{
		public async Task<string> HashPasswordAsync(string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				throw new BusinessLogicException("Empty password");
			}

			var salt = Environment.GetEnvironmentVariable("PASSWORD_SALT");

			if (string.IsNullOrEmpty(salt))
			{
				throw new ApplicationErrorException("Salt for hash password is empty");
			}

			try
			{
				using var sha256 = SHA256.Create();

				using var stream = new MemoryStream(Encoding.UTF8.GetBytes(password + salt));

				var hash = await sha256.ComputeHashAsync(stream);

				var hashPassword = Convert.ToHexString(hash);

				return hashPassword;
			}
			catch (Exception e)
			{
				throw new ApplicationErrorException("HashPassword Error", e);
			}
		}
	}
}
