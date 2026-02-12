namespace UserCurrency.Common.Options
{
	/// <summary>
	/// Дополнительные опции JWT авторизации
	/// </summary>
	public class AuthOptions
	{
		/// <summary>
		/// Время жизни токена в минутах
		/// </summary>
		public int ExpirationTimeMin { get; set; } = 60 * 24 * 30; // 30 days
	}
}
