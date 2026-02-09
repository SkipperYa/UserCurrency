using UserCurrency.Common.Models;

namespace UserService.Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; } = "";
	}
}
