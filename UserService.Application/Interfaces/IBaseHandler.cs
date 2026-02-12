namespace UserService.Application.Interfaces
{
	/// <summary>
	/// Базовый интерфейс обработчика
	/// </summary>
	/// <typeparam name="T">Тип команды или запроса</typeparam>
	/// <typeparam name="P">Тип результата</typeparam>
	public interface IBaseHandler<T, P>
		where T : class, IBaseCommand
		where P : class
	{
		Task<P> HandleAsync(T command, CancellationToken cancellationToken);
	}
}
