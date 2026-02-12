using UserService.Application.Commands;

namespace UserService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс обработчика авторизации пользователя
	/// </summary>
	public interface ILoginHandler : IBaseHandler<LoginUserCommand, string>
	{
	}
}
