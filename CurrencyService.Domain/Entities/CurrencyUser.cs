using UserCurrency.Common.Models;

namespace CurrencyService.Domain.Entities
{
	public class CurrencyUser : BaseEntity
	{
		public long UserId { get; set; }
		public long CurrencyId { get; set; }
		public Currency Currency { get; set; } = new();
	}
}
