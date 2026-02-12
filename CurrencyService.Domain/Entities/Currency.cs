using UserCurrency.Common.Models;

namespace CurrencyService.Domain.Entities
{
	/// <summary>
	/// Валюта
	/// </summary>
	public class Currency : BaseEntity
	{
		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; } = "";
		/// <summary>
		/// Значение
		/// </summary>
		public decimal Rate { get; set; }
	}
}
