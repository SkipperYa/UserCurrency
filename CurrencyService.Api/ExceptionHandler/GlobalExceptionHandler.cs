using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserCurrency.Common.Exceptions;

namespace CurrencyService.Api.ExceptionHandler
{
	/// <summary>
	/// Глобальный обработчик исключений
	/// </summary>
	public class GlobalExceptionHandler : IExceptionHandler
	{
		private readonly ILogger<GlobalExceptionHandler> logger;

		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
		{
			this.logger = logger;
		}

		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			var problemDetails = MapExceptionToProblemDetails(httpContext, exception);

			if (exception is not BusinessLogicException)
			{
				logger.LogError(exception, "Internal Server Error");
			}

			httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
			await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

			return true;
		}

		/// <summary>
		/// Сформировать ProblemDetails по исключению
		/// </summary>
		/// <param name="httpContext"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		private ProblemDetails MapExceptionToProblemDetails(HttpContext httpContext, Exception exception)
		{
			return exception switch
			{
				BusinessLogicException => new ProblemDetails
				{
					Status = (int)HttpStatusCode.BadRequest,
					Title = "Business Logic Error",
					Detail = exception.Message,
					Instance = httpContext.Request.Path
				},
				ApplicationErrorException => new ProblemDetails
				{
					Status = (int)HttpStatusCode.InternalServerError,
					Title = "Internal Application Error",
					Detail = "An error occurred.",
					Instance = httpContext.Request.Path
				},
				_ => new ProblemDetails
				{
					Status = (int)HttpStatusCode.InternalServerError,
					Title = "Internal Server Error",
					Detail = "An unexpected error occurred.",
					Instance = httpContext.Request.Path
				}
			};
		}
	}
}
