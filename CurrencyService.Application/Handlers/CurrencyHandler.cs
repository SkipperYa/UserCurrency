using CurrencyService.Application.Interfaces;
using CurrencyService.Application.Queries;
using CurrencyService.Domain.Entities;
using UserCurrency.Common.Exceptions;

namespace CurrencyService.Application.Handlers
{
	/// <summary>
	/// Реализация обработчика получения валют пользователя
	/// </summary>
	public class CurrencyHandler : ICurrencyHandler
	{
		private readonly ICurrencyRepository repository;

		public CurrencyHandler(ICurrencyRepository repository)
		{
			this.repository = repository;
		}

		public async Task<List<Currency>> HandleAsync(CurrencyUserQuery command, CancellationToken cancellationToken)
		{
			if (command.UserId <= 0)
			{
				throw new BusinessLogicException("UserId is required.");
			}

			var currencyUserList = await repository.GetUserCurrenciesAsync(command.UserId, cancellationToken);

			return currencyUserList.Select(q => q.Currency).ToList();
		}
	}
}
