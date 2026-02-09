namespace UserService.Application.Interfaces
{
	public interface IHashPasswordService
	{
		Task<string> HashPasswordAsync(string password);
	}
}
