using CurrencyService.Application.Interfaces;
using CurrencyService.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using UserCurrency.Common.Models;

namespace CurrencyService.Api.Controllers
{
	[Route("[controller]")]
	public class CurrencyController : BaseAuthorizedApiController
	{
		private readonly ICurrencyHandler currencyHandler;

		public CurrencyController(ICurrencyHandler currencyHandler)
		{
			this.currencyHandler = currencyHandler;
		}

		[HttpGet]
		public async Task<IActionResult> GetUserCurrencies(CancellationToken cancellationToken)
		{
			var currencies = await currencyHandler.HandleAsync(new CurrencyUserQuery() { UserId = UserId }, cancellationToken);
			return Ok(new Response() { Value = currencies });
		}
	}
}
