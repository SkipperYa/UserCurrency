using Microsoft.AspNetCore.Mvc;
using UserCurrency.Common.Models;
using UserService.Application.Commands;
using UserService.Application.Dto;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
	/// <summary>
	/// Контроллер пользователя
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IRegistrationUserHandler handler;

		public UserController(IRegistrationUserHandler handler)
		{
			this.handler = handler;
		}
		
		/// <summary>
		/// Регистрация пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost("registration")]
		public async Task<IActionResult> Registration([FromBody] RegistrationUserDto dto, CancellationToken cancellationToken)
		{
			await handler.HandleAsync(new RegistrationUserCommand() { Name = dto.Name, Password = dto.Password }, cancellationToken);
			
			return Ok(new Response());
		}
	}
}
