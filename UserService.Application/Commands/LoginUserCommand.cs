using UserCurrency.Common.Interfaces;

namespace UserService.Application.Commands
{
	/// <summary>
	/// Команда авторизации пользователя
	/// </summary>
	public class LoginUserCommand : IBaseCommand
	{
		public string UserName { get; set; } = "";
		public string Password { get; set; } = "";
	}
}
