using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces
{
	public interface ICurrencyRepository
	{
		Task<List<CurrencyUser>> GetUserCurrencies(long userId, CancellationToken cancellationToken);
		Task<Currency> CreateOrUpdate(Currency item, CancellationToken cancellationToken);
	}
}
