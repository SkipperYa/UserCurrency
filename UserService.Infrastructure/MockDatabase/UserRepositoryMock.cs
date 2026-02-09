using UserCurrency.Common.Exceptions;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.MockDatabase
{
	public class UserRepositoryMock : IUserRepository
	{
		private readonly HashSet<User> Store;

		public UserRepositoryMock()
		{
			Store = [new User() { Id = 1, Name = "Admin" }];
		}

		public Task<bool> CheckLoginAsync(string userName, string hashPassword, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task<User> CreateAsync(User item, string hashPassword, CancellationToken cancellationToken)
		{
			await Task.Yield();

			if (Store.Add(item))
			{
				return item;
			}

			throw new BusinessLogicException("Cant create user");
		}
	}
}
