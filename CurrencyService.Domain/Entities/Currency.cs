using UserCurrency.Common.Models;

namespace CurrencyService.Domain.Entities
{
	public class Currency : BaseEntity
	{
		public string Name { get; set; } = "";
		public decimal Rate { get; set; }
	}
}
