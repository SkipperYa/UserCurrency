namespace UserCurrency.BackgroundWorker.Options
{
	public class CurrencyHostedServiceOptions
	{
		public TimeSpan Delay { get; set; } = TimeSpan.FromMinutes(5);
	}
}
