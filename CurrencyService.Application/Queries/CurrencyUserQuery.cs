using CurrencyService.Application.Interfaces;
using UserCurrency.Common.Interfaces;

namespace CurrencyService.Application.Queries
{
	/// <summary>
	/// Запрос на получение валют пользователя
	/// </summary>
	public class CurrencyUserQuery : IBaseQuery
	{
		public long UserId { get; set; }
	}
}
