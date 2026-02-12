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

			var user = await repository.GetByLoginAndPasswordAsync(command.UserName, hasPassword, cancellationToken)
				?? throw new ApplicationErrorException($"Invalid login or password.");

			var token = jwtTokenService.GetJwtToken(user.Id);

			return token;
		}
	}
}
