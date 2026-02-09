using UserService.Application.Interfaces;

namespace UserService.Application.Commands
{
	public class RegistrationUserCommand : IBaseCommand
	{
		public string Name { get; set; } = "";
		public string Password { get; set; } = "";
	}
}
