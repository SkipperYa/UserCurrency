using UserCurrency.Common.Interfaces;
using UserService.Application.Commands;

namespace UserService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс обработчика авторизации пользователя
	/// </summary>
	public interface ILoginHandler : IBaseCommandHandler<LoginUserCommand, string>
	{
	}
}
