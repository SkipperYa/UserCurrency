using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс репозитория пользователя
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Создать пользователя
		/// </summary>
		/// <param name="item"></param>
		/// <param name="hashPassword"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<User> CreateAsync(User item, string hashPassword, CancellationToken cancellationToken);
		/// <summary>
		/// Загрузить пользователя по логину и паролю
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="hashPassword"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<User?> GetByLoginAndPasswordAsync(string userName, string hashPassword, CancellationToken cancellationToken);
	}
}
