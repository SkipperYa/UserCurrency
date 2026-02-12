namespace UserService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса хэширования пароля
	/// </summary>
	public interface IHashPasswordService
	{
		/// <summary>
		/// Хэшировать пароль
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		Task<string> HashPasswordAsync(string password);
	}
}
