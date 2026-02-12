using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс репозитория валют пользователя
	/// </summary>
	public interface ICurrencyRepository
	{
		/// <summary>
		/// Получить валюты пользователя
		/// </summary>
		/// <param name="userId">Уникальный идентификатор пользователя</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<List<CurrencyUser>> GetUserCurrenciesAsync(long userId, CancellationToken cancellationToken);
		/// <summary>
		/// Создать или обновить существующую валюту
		/// </summary>
		/// <param name="item">Валюта</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<Currency> CreateOrUpdateAsync(Currency item, CancellationToken cancellationToken);
	}
}
