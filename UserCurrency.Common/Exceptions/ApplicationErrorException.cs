namespace UserCurrency.Common.Exceptions
{
	/// <summary>
	/// Критическое исключение приложения, логируется и не выводится пользователю
	/// </summary>
	public class ApplicationErrorException : Exception
	{
		public ApplicationErrorException(string message) : base(message) { }
		public ApplicationErrorException(string message, Exception inner) : base(message, inner) { }
	}
}
