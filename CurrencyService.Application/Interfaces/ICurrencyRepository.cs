using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces
{
	public interface ICurrencyRepository
	{
		Task<List<CurrencyUser>> GetUserCurrenciesAsync(long userId, CancellationToken cancellationToken);
		Task<Currency> CreateOrUpdateAsync(Currency item, CancellationToken cancellationToken);
	}
}
