using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CurrencyService.Api.Controllers
{
	[ApiController]
	[Authorize]
	public abstract class BaseAuthorizedApiController : ControllerBase
	{
		protected long UserId => User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) && long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "", out var id)
			? id
			: 0;
	}
}
