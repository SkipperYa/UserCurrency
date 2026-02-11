using UserCurrency.Common.Exceptions;
using UserService.Application.Commands;
using UserService.Application.Interfaces;

namespace UserService.Application.Handlers
{
	public class LoginHandler : ILoginHandler
	{
		private readonly IUserRepository repository;
		private readonly IHashPasswordService hashPasswordService;
		private readonly IJwtTokenService jwtTokenService;

		public LoginHandler(IUserRepository repository, IHashPasswordService hashPasswordService, IJwtTokenService jwtTokenService)
		{
			this.repository = repository;
			this.hashPasswordService = hashPasswordService;
			this.jwtTokenService = jwtTokenService;
		}

		public async Task<string> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(command.UserName))
			{
				throw new BusinessLogicException("User Name is required.");
			}

			if (string.IsNullOrEmpty(command.Password))
			{
				throw new BusinessLogicException("User Password is required.");
			}

			var hasPassword = await hashPasswordService.HashPasswordAsync(command.Password);

			var success = await repository.CheckLoginAsync(command.UserName, hasPassword, cancellationToken);

			if (!success)
			{
				throw new BusinessLogicException("Invalid login or password");
			}

			var user = await repository.GetByNameAsync(command.UserName, cancellationToken)
				?? throw new ApplicationErrorException($"Cant find user by Name {command.UserName}");

			var token = jwtTokenService.GetJwtToken(user.Id);

			return token;
		}
	}
}
