using System.Security.Cryptography;
using System.Text;
using UserCurrency.Common.Exceptions;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Services
{
	public class HashPasswordService : IHashPasswordService
	{
		private const string _salt = "ZjDKxmlFKNoa";
		public async Task<string> HashPasswordAsync(string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				throw new BusinessLogicException("Empty password");
			}

			try
			{
				using var sha256 = SHA256.Create();

				using var stream = new MemoryStream(Encoding.UTF8.GetBytes(password + _salt));

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
