using CurrencyService.Application.Interfaces;
using CurrencyService.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using UserCurrency.BackgroundWorker.Options;

namespace UserCurrency.BackgroundWorker.BackgroundServices
{
	public class CurrencyUpdaterHostedService : BackgroundService
	{
		private readonly IOptions<CurrencyHostedServiceOptions> options;
		private readonly ILogger<CurrencyUpdaterHostedService> logger;
		private readonly IHttpClientFactory httpClientFactory;
		private readonly IServiceProvider serviceProvider;

		public CurrencyUpdaterHostedService(
			IOptions<CurrencyHostedServiceOptions> options,
			ILogger<CurrencyUpdaterHostedService> logger,
			IHttpClientFactory httpClientFactory,
			IServiceProvider serviceProvider)
		{
			this.options = options;
			this.logger = logger;
			this.httpClientFactory = httpClientFactory;
			this.serviceProvider = serviceProvider;
		}

		private async Task UpdateCurrenciesAsync(CancellationToken stoppingToken)
		{
			var client = httpClientFactory.CreateClient("DailyCurrencyClient");

			using var response = await client.GetAsync("/scripts/XML_daily.asp", stoppingToken);
			response.EnsureSuccessStatusCode();

			using var stream = await response.Content.ReadAsStreamAsync(stoppingToken);

			using var reader = new StreamReader(stream, encoding: Encoding.GetEncoding("windows-1251"));

			var xmlContent = await reader.ReadToEndAsync(stoppingToken);

			var xdoc = XDocument.Parse(xmlContent);

			var currencies = xdoc.Descendants("Valute")
				.Select(x => new Currency
				{
					Name = x.Element("Name")?.Value ?? string.Empty,
					Rate = decimal.Parse(x.Element("Value")?.Value ?? "0.0", NumberStyles.Any)
				}).ToList();

			using var scope = serviceProvider.CreateScope();
			var repository = scope.ServiceProvider.GetRequiredService<ICurrencyRepository>();

			foreach (var currency in currencies)
			{
				await repository.CreateOrUpdate(currency, stoppingToken);
			}
		}

		protected async override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					await UpdateCurrenciesAsync(stoppingToken);
				}
				catch (OperationCanceledException)
				{

				}
				catch (Exception e)
				{
					logger.LogError(e, "Error while update currency");
				}

				try
				{
					await Task.Delay(options.Value.Delay, stoppingToken);
				}
				catch (OperationCanceledException)
				{

				}
				catch (Exception e)
				{
					logger.LogError(e, "Error while delay waiting update currency");
				}
			}
		}
	}
}
