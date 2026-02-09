using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.Dto;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IRegistrationUserHandler handler;

		public UserController(IRegistrationUserHandler handler)
		{
			this.handler = handler;
		}

		[HttpPost("registration")]
		public async Task<IActionResult> Registration([FromBody] RegistrationUserDto dto, CancellationToken cancellationToken)
		{
			var result = await handler.HandleAsync(new RegistrationUserCommand() { Name = dto.Name, Password = dto.Password }, cancellationToken);
			
			return Ok();
		}
	}
}
