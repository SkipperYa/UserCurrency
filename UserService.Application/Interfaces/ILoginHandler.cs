using UserService.Application.Commands;

namespace UserService.Application.Interfaces
{
	public interface ILoginHandler : IBaseHandler<LoginUserCommand, string>
	{
	}
}
