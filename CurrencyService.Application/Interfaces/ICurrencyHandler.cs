using CurrencyService.Application.Queries;
using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces
{
	public interface ICurrencyHandler : IBaseHandler<CurrencyUserQuery, List<Currency>>
	{
	}
}
