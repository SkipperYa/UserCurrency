using CurrencyService.Application.Interfaces;

namespace CurrencyService.Application.Queries
{
	public class CurrencyUserQuery : IBaseQuery
	{
		public long UserId { get; set; }
	}
}
