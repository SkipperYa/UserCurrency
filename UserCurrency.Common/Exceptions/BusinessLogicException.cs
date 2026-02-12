namespace UserCurrency.Common.Exceptions
{
	/// <summary>
	/// Исключение логики работы приложения, не логируется и выводится пользователю
	/// </summary>
	public class BusinessLogicException : Exception
	{
		public BusinessLogicException(string message) : base(message) { }
		public BusinessLogicException(string message, Exception inner) : base(message, inner) { }
	}
}
