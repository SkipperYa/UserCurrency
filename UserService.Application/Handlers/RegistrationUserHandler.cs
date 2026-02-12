using UserCurrency.Common.Exceptions;
using UserService.Application.Commands;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Handlers
{
	public class RegistrationUserHandler : IRegistrationUserHandler
	{
		private readonly IUserRepository repository;
		private readonly IHashPasswordService hashPasswordService;

		public RegistrationUserHandler(IUserRepository repository, IHashPasswordService hashPasswordService)
		{
			this.repository = repository;
			this.hashPasswordService = hashPasswordService;
		}

		public async Task<User> HandleAsync(RegistrationUserCommand command, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(command.Name))
			{
				throw new BusinessLogicException("User Name is required.");
			}

			if (string.IsNullOrEmpty(command.Password))
			{
				throw new BusinessLogicException("User Password is required.");
			}

			var hashPassword = await hashPasswordService.HashPasswordAsync(command.Password);

			var user = await repository.CreateAsync(new User() { Name = command.Name }, hashPassword, cancellationToken);

			if (user.Id <= 0)
			{
				throw new BusinessLogicException("Cant create user.");
			}

			return user;
		}
	}
}
