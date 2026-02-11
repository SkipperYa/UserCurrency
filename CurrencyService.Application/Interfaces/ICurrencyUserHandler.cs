using CurrencyService.Application.Queries;
using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces
{
	public interface ICurrencyUserHandler : IBaseHandler<CurrencyUserQuery, List<Currency>>
	{
	}
}
