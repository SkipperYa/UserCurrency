using CurrencyService.Application.Queries;
using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс обработчика получения валют пользователя
	/// </summary>
	public interface ICurrencyHandler : IBaseHandler<CurrencyUserQuery, List<Currency>>
	{
	}
}
