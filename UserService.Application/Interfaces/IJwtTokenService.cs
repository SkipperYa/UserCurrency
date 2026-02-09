namespace UserService.Application.Interfaces
{
	public interface IJwtTokenService
	{
		string GetJwtToken(long userId);
	}
}
