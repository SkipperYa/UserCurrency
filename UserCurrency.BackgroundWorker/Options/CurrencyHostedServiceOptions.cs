namespace UserCurrency.BackgroundWorker.Options
{
	/// <summary>
	/// Опции сервиса обновления валют пользователя
	/// </summary>
	public class CurrencyHostedServiceOptions
	{
		/// <summary>
		/// Время задержки работы сервиса в минутах (в среднем можно установить 1 день)
		/// </summary>
		public TimeSpan Delay { get; set; } = TimeSpan.FromMinutes(60 * 24);
	}
}
