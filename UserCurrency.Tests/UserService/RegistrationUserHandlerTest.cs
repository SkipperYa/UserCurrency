using Moq;
using UserCurrency.Common.Exceptions;
using UserService.Application.Commands;
using UserService.Application.Handlers;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserCurrency.Tests.UserService
{
	[TestClass]
	public class RegistrationUserHandlerTest
	{
		private Mock<IUserRepository> repository { get; set; } = null!;
		private Mock<IHashPasswordService> hashPasswordService { get; set; } = null!;
		private IRegistrationUserHandler handler { get; set; } = null!;

		[TestInitialize]
		public void Setup()
		{
			repository = new Mock<IUserRepository>();
			hashPasswordService = new Mock<IHashPasswordService>();
			handler = new RegistrationUserHandler(repository.Object, hashPasswordService.Object);
		}

		[TestMethod]
		public async Task CreateUserWithValidData()
		{
			var command = new RegistrationUserCommand() { Name = "test", Password = "test" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.CreateAsync(It.IsAny<User>(), "hashed_password", default))
				.ReturnsAsync(new User() { Id = 1, Name = "test" });

			var result = await handler.HandleAsync(command, default);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Id);
			Assert.AreEqual("test", result.Name);
		}

		[TestMethod]
		public async Task CreateUserWithInvalidName()
		{
			var command = new RegistrationUserCommand() { Name = "", Password = "test" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.CreateAsync(It.IsAny<User>(), "hashed_password", default))
				.ReturnsAsync(new User() { Id = 1, Name = "test" });

			var exception = await Assert.ThrowsAsync<BusinessLogicException>(() => handler.HandleAsync(command, default));

			Assert.IsNotNull(exception);
			Assert.AreEqual("User Name is required.", exception.Message);
		}

		[TestMethod]
		public async Task CreateUserWithInvalidPassword()
		{
			var command = new RegistrationUserCommand() { Name = "test", Password = "" };

			hashPasswordService
				.Setup(q => q.HashPasswordAsync(command.Password))
				.ReturnsAsync("hashed_password");

			repository
				.Setup(q => q.CreateAsync(It.IsAny<User>(), "hashed_password", default))
				.ReturnsAsync(new User() { Id = 1, Name = "test" });

			var exception = await Assert.ThrowsAsync<BusinessLogicException>(() => handler.HandleAsync(command, default));

			Assert.IsNotNull(exception);
			Assert.AreEqual("User Password is required.", exception.Message);
		}
	}
}
