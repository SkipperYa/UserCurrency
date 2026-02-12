using Moq;
using UserCurrency.Common.Exceptions;
using UserService.Application.Commands;
using UserService.Application.Handlers;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserCurrency.Tests.UserService
{
	/// <summary>
	/// Набор тестов для микросервиса пользователя
	/// </summary>
	[TestClass]
	public class LoginUserHandlerTest
	{
		private Mock<IUserRepository> repository { get; set; } = null!;
		private Mock<IHashPasswordService> hashPasswordService { get; set; } = null!;
		private Mock<IJwtTokenService> jwtTokenService { get; set; } = null!;
		private ILoginHandler handler { get; set; } = null!;

		[TestInitialize]
		public void Setup()
		{
			repository = new Mock<IUserRepository>();
			hashPasswordService = new Mock<IHashPasswordService>();
			jwtTokenService = new Mock<IJwtTokenService>();
			handler = new LoginHandler(repository.Object, hashPasswordService.Object, jwtTokenService.Object);
		}

		/// <summary>
		/// Тест на успешную авторизацию
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task LoginWithValidData()
		{
			var command = new LoginUserCommand() { UserName = "test", Password = "test" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.GetByLoginAndPasswordAsync("test", "hashed_password", default))
				.ReturnsAsync(new User() { Id = 1, Name = "test" });

			jwtTokenService
				.Setup(q => q.GetJwtToken(1))
				.Returns("token");

			var result = await handler.HandleAsync(command, default);

			Assert.IsNotNull(result);
			Assert.AreEqual("token", result);
		}

		/// <summary>
		/// Тест на авторизацию без логина
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task LoginWithEmptyUserName()
		{
			var command = new LoginUserCommand() { UserName = "", Password = "test" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.GetByLoginAndPasswordAsync("test", "hashed_password", default))
				.ReturnsAsync(new User() { Id = 1, Name = "test" });

			jwtTokenService
				.Setup(q => q.GetJwtToken(1))
				.Returns("token");

			var exception = await Assert.ThrowsAsync<BusinessLogicException>(() => handler.HandleAsync(command, default));

			Assert.IsNotNull(exception);
			Assert.AreEqual("User Name is required.", exception.Message);
		}

		/// <summary>
		/// Тест на авторизацию без пароля
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task LoginWithEmptyPassword()
		{
			var command = new LoginUserCommand() { UserName = "test", Password = "" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.GetByLoginAndPasswordAsync("test", "hashed_password", default))
				.ReturnsAsync(new User() { Id = 1, Name = "test" });

			jwtTokenService
				.Setup(q => q.GetJwtToken(1))
				.Returns("token");

			var exception = await Assert.ThrowsAsync<BusinessLogicException>(() => handler.HandleAsync(command, default));

			Assert.IsNotNull(exception);
			Assert.AreEqual("User Password is required.", exception.Message);
		}

		/// <summary>
		/// Тест на авторизацию с неверным логином
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task LoginWithWrongUserName()
		{
			var command = new LoginUserCommand() { UserName = "test1", Password = "test" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.GetByLoginAndPasswordAsync("test", "hashed_password", default))
				.ReturnsAsync((User?)null);

			jwtTokenService
				.Setup(q => q.GetJwtToken(1))
				.Returns("token");

			var exception = await Assert.ThrowsAsync<BusinessLogicException>(() => handler.HandleAsync(command, default));

			Assert.IsNotNull(exception);
			Assert.AreEqual("Invalid login or password.", exception.Message);
		}

		/// <summary>
		/// Тест на авторизацию с неверным паролем
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task LoginWithWrongPassword()
		{
			var command = new LoginUserCommand() { UserName = "test", Password = "test1" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.GetByLoginAndPasswordAsync("test", "hashed_password", default))
				.ReturnsAsync((User?)null);

			jwtTokenService
				.Setup(q => q.GetJwtToken(1))
				.Returns("token");

			var exception = await Assert.ThrowsAsync<BusinessLogicException>(() => handler.HandleAsync(command, default));

			Assert.IsNotNull(exception);
			Assert.AreEqual("Invalid login or password.", exception.Message);
		}
	}
}
