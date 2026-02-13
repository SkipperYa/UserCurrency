using CurrencyService.Application.Queries;
using CurrencyService.Domain.Entities;
using UserCurrency.Common.Interfaces;

namespace CurrencyService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс обработчика получения валют пользователя
	/// </summary>
	public interface ICurrencyHandler : IBaseQueryHandler<CurrencyUserQuery, List<Currency>>
	{
	}
}
