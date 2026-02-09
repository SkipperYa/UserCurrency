namespace UserService.Application.Interfaces
{
	public interface IBaseHandler<T, P>
		where T : class, IBaseCommand
		where P : class
	{
		Task<P> HandleAsync(T command, CancellationToken cancellationToken);
	}
}
