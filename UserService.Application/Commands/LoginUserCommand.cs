using UserService.Application.Interfaces;

namespace UserService.Application.Commands
{
	public class LoginUserCommand : IBaseCommand
	{
		public string UserName { get; set; } = "";
		public string Password { get; set; } = "";
	}
}
