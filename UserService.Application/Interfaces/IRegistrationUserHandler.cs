using UserService.Application.Commands;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
	public interface IRegistrationUserHandler : IBaseHandler<RegistrationUserCommand, User>
	{
	}
}
