using UserCurrency.Common.Models;

namespace UserService.Domain.Entities
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class User : BaseEntity
	{
		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string Name { get; set; } = "";
	}
}
