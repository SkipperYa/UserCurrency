namespace CurrencyService.Application.Interfaces
{
	public interface IBaseHandler<T, P>
		where T : class, IBaseQuery
		where P : class
	{
		Task<P> HandleAsync(T command, CancellationToken cancellationToken);
	}
}
