using CurrencyService.Application.Interfaces;
using CurrencyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UserCurrency.Common.Exceptions;

namespace CurrencyService.Infrastructure.Database
{
	public class CurrencyUserRepository : ICurrencyUserRepository
	{
		private readonly CurrencyDbContext applicationContext;

		public CurrencyUserRepository(CurrencyDbContext applicationContext)
		{
			this.applicationContext = applicationContext;
		}

		public async Task<List<CurrencyUser>> GetUserCurrencies(long userId, CancellationToken cancellationToken)
		{
			try
			{
				return await applicationContext.UserCurrencies
					.Include(q => q.Currency)
					.Where(q => q.UserId == userId)
					.ToListAsync(cancellationToken);
			}
			catch (Exception e)
			{
				throw new ApplicationErrorException("Error while loading user currencies", e);
			}
		}
	}
}
