using CurrencyService.Application.Interfaces;
using CurrencyService.Application.Queries;
using CurrencyService.Domain.Entities;
using UserCurrency.Common.Exceptions;

namespace CurrencyService.Application.Handlers
{
	public class CurrencyUserHandler : ICurrencyUserHandler
	{
		private readonly ICurrencyUserRepository repository;

		public CurrencyUserHandler(ICurrencyUserRepository repository)
		{
			this.repository = repository;
		}

		public async Task<List<Currency>> HandleAsync(CurrencyUserQuery command, CancellationToken cancellationToken)
		{
			if (command.UserId <= 0)
			{
				throw new BusinessLogicException("UserId is required");
			}

			var currencyUserList = await repository.GetUserCurrencies(command.UserId, cancellationToken);

			return currencyUserList.Select(q => q.Currency).ToList();
		}
	}
}
