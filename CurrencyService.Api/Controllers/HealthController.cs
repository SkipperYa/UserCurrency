using Microsoft.AspNetCore.Mvc;

namespace CurrencyService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Ok currency-service");
		}
	}
}
