using CurrencyService.Application.Handlers;
using CurrencyService.Application.Interfaces;
using CurrencyService.Application.Queries;
using CurrencyService.Domain.Entities;
using Moq;
using UserCurrency.Common.Exceptions;

namespace UserCurrency.Tests.CurrencyService
{
	/// <summary>
	/// Набор тестов для микросервиса валют
	/// </summary>
	[TestClass]
	public class CurrencyHandlerTest
	{
		private Mock<ICurrencyRepository> repository { get; set; } = null!;
		private ICurrencyHandler handler { get; set; } = null!;

		[TestInitialize]
		public void Setup()
		{
			repository = new Mock<ICurrencyRepository>();
			handler = new CurrencyHandler(repository.Object);
		}

		/// <summary>
		/// Тест на успешное получение валют пользователя
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task GetUserCurrenciesSuccess()
		{
			var command = new CurrencyUserQuery() { UserId = 1 };

			var list = new List<CurrencyUser>()
			{
				new CurrencyUser()
				{
					Id = 1,
					UserId = 1,
					Currency = new Currency { Id = 1, Name = "Test", Rate = 14.7m }
				}
			};

			repository
				.Setup(q => q.GetUserCurrenciesAsync(1, default))
				.ReturnsAsync(list);

			var result = await handler.HandleAsync(command, default);

			var expected = new List<Currency>()
			{
				new Currency { Id = 1, Name = "Test", Rate = 14.7m }
			};

			Assert.IsNotNull(result);
			Assert.HasCount(expected.Count, result);
			Assert.IsNotNull(result[0]);
			Assert.AreEqual(expected[0].Id, result[0].Id);
			Assert.AreEqual(expected[0].Name, result[0].Name);
			Assert.AreEqual(expected[0].Rate, result[0].Rate);
		}

		/// <summary>
		/// Тест на неудачное получение валют без идентификатора пользователя
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task GetUserCurrenciesWithWrongUserId()
		{
			var command = new CurrencyUserQuery() { };

			var expected = new List<CurrencyUser>()
			{
				new CurrencyUser()
				{
					Id = 1,
					UserId = 1,
					Currency = new Currency { Id = 1, Name = "Test", Rate = 14.7m }
				}
			};

			repository
				.Setup(q => q.GetUserCurrenciesAsync(1, default))
				.ReturnsAsync(expected);

			var exception = await Assert.ThrowsAsync<BusinessLogicException>(() => handler.HandleAsync(command, default));

			Assert.IsNotNull(exception);
			Assert.AreEqual("UserId is required.", exception.Message);
		}
	}
}
