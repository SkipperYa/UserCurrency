using UserCurrency.Common.Models;

namespace CurrencyService.Domain.Entities
{
	/// <summary>
	/// Связь валюты и пользователя
	/// </summary>
	public class CurrencyUser : BaseEntity
	{
		/// <summary>
		/// Уникальный идентификатор пользователя
		/// </summary>
		public long UserId { get; set; }
		/// <summary>
		/// Уникальный идентификатор валюты
		/// </summary>
		public long CurrencyId { get; set; }
		/// <summary>
		/// Навигационное св-во валюты
		/// </summary>
		public Currency Currency { get; set; } = new();
	}
}
