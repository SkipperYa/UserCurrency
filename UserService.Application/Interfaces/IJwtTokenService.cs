namespace UserService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс генерации JWT токена
	/// </summary>
	public interface IJwtTokenService
	{
		/// <summary>
		/// Сгенерировать токен
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		string GetJwtToken(long userId);
	}
}
