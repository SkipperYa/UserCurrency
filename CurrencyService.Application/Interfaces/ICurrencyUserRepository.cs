using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces
{
	public interface ICurrencyUserRepository
	{
		Task<List<CurrencyUser>> GetUserCurrencies(long userId, CancellationToken cancellationToken);
	}
}
