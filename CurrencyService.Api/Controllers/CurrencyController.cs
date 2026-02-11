using CurrencyService.Application.Interfaces;
using CurrencyService.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserCurrency.Common.Models;

namespace CurrencyService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class CurrencyController : ControllerBase
	{
		private readonly ICurrencyUserHandler currencyUserHandler;
		private long userId => User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) && long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "", out var id)
			? id
			: 0;

		public CurrencyController(ICurrencyUserHandler currencyUserHandler)
		{
			this.currencyUserHandler = currencyUserHandler;
		}

		[HttpGet]
		public async Task<IActionResult> GetUserCurrencies(CancellationToken cancellationToken)
		{
			var currencies = await currencyUserHandler.HandleAsync(new CurrencyUserQuery() { UserId = userId }, cancellationToken);
			return Ok(new Response() { Value = currencies });
		}
	}
}
