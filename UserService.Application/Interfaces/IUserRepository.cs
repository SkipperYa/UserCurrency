using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
	public interface IUserRepository
	{
		Task<User> CreateAsync(User item, string hashPassword, CancellationToken cancellationToken);
		Task<User?> GetByLoginAndPasswordAsync(string userName, string hashPassword, CancellationToken cancellationToken);
	}
}
