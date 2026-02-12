using Microsoft.AspNetCore.Mvc;
using UserCurrency.Common.Models;
using UserService.Application.Commands;
using UserService.Application.Dto;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
	/// <summary>
	/// Логин контролер
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class LoginController : ControllerBase
	{
		private readonly ILoginHandler handler;

		public LoginController(ILoginHandler handler)
		{
			this.handler = handler;
		}

		/// <summary>
		/// Авторизация пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
		{
			var result = await handler.HandleAsync(new LoginUserCommand() { UserName = dto.UserName, Password = dto.Password }, cancellationToken);

			return Ok(new Response() { Value = new { Token = result } });
		}

		/// <summary>
		/// Логаут
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Logout(CancellationToken cancellationToken)
		{
			// Вернуть 200, так как у токена есть вермя жизни
			return Ok();
		}
	}
}
