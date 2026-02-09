using Microsoft.AspNetCore.Mvc;
using UserCurrency.Common.Models;
using UserService.Application.Commands;
using UserService.Application.Dto;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LoginController : ControllerBase
	{
		private readonly ILoginHandler handler;

		public LoginController(ILoginHandler handler)
		{
			this.handler = handler;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
		{
			var result = await handler.HandleAsync(new LoginUserCommand() { UserName = dto.UserName, Password = dto.Password }, cancellationToken);

			return Ok(new Response() { Value = new { Token = result } });
		}

		[HttpGet]
		public IActionResult Logout(CancellationToken cancellationToken)
		{
			return Ok();
		}
	}
}
