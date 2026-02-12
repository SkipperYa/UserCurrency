using CurrencyService.Application.Interfaces;
using CurrencyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using UserCurrency.Common.Exceptions;

namespace CurrencyService.Infrastructure.Database
{
	public class CurrencyRepository : ICurrencyRepository
	{
		private readonly CurrencyDbContext applicationContext;

		public CurrencyRepository(CurrencyDbContext applicationContext)
		{
			this.applicationContext = applicationContext;
		}

		public async Task<List<CurrencyUser>> GetUserCurrenciesAsync(long userId, CancellationToken cancellationToken)
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

		public async Task<Currency> CreateOrUpdateAsync(Currency item, CancellationToken cancellationToken)
		{
			try
			{
				var existingEntity = await applicationContext.Currencies
					.FirstOrDefaultAsync(q => q.Name == item.Name, cancellationToken);

				if (existingEntity != null)
				{
					if (existingEntity.Rate == item.Rate)
					{
						return existingEntity;
					}

					existingEntity.Rate = item.Rate;
				}
				else
				{
					applicationContext.Currencies.Add(item);
				}

				await applicationContext.SaveChangesAsync(cancellationToken);

				return existingEntity ?? item;
			}
			catch (DbUpdateException dbue)
			{
				if (dbue.InnerException is PostgresException pg && pg.SqlState == PostgresErrorCodes.UniqueViolation)
				{
					throw new BusinessLogicException($"Currency with same Name \"{item.Name}\" already exist");
				}
				else
				{
					throw new ApplicationErrorException($"Create currency error", dbue);
				}
			}
			catch (Exception e)
			{
				throw new ApplicationErrorException("Error while create currency", e);
			}
		}
	}
}
