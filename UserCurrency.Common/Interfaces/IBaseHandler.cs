namespace UserCurrency.Common.Interfaces
{
	/// <summary>
	/// Базовый интерфейс обработчика
	/// </summary>
	/// <typeparam name="T">Тип команды или запроса</typeparam>
	/// <typeparam name="P">Тип результата</typeparam>
	public interface IBaseCommandHandler<T, P>
		where T : class, IBaseCommand
		where P : class
	{
		Task<P> HandleAsync(T command, CancellationToken cancellationToken);
	}
	/// <summary>
	/// Базовый интерфейс обработчика
	/// </summary>
	/// <typeparam name="T">Тип команды или запроса</typeparam>
	/// <typeparam name="P">Тип результата</typeparam>
	public interface IBaseQueryHandler<T, P>
		where T : class, IBaseQuery
		where P : class
	{
		Task<P> HandleAsync(T command, CancellationToken cancellationToken);
	}
}
