namespace UserCurrency.Common.Models
{
	/// <summary>
	/// Базовая сущность
	/// </summary>
	public abstract class BaseEntity
	{
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public long Id { get; set; }
	}
}
