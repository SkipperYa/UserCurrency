using UserCurrency.Common.Interfaces;

namespace UserService.Application.Commands
{
	/// <summary>
	/// Команда регистрации пользователя
	/// </summary>
	public class RegistrationUserCommand : IBaseCommand
	{
		public string Name { get; set; } = "";
		public string Password { get; set; } = "";
	}
}
