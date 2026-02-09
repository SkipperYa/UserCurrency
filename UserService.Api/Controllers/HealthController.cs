using Microsoft.AspNetCore.Mvc;

namespace UserService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthController : ControllerBase
	{
		[HttpGet("user-service")]
		public IActionResult Get()
		{
			return Ok("Ok user-service");
		}
	}
}
