using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
	public interface IUserRepository
	{
		Task<User> CreateAsync(User item, string hashPassword, CancellationToken cancellationToken);
		Task<User?> GetByNameAsync(string userName, CancellationToken cancellationToken);
		Task<bool> CheckLoginAsync(string userName, string hashPassword, CancellationToken cancellationToken);
	}
}
