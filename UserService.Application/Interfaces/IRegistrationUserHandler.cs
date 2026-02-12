using UserService.Application.Commands;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
	/// <summary>
	/// Интерфейс обработчика регистрации пользователя
	/// </summary>
	public interface IRegistrationUserHandler : IBaseHandler<RegistrationUserCommand, User>
	{
	}
}
