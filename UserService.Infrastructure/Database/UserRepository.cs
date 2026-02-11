using Microsoft.EntityFrameworkCore;
using Npgsql;
using UserCurrency.Common.Exceptions;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Database
{
	public class UserRepository : IUserRepository
	{
		private readonly UserDbContext applicationContext;

		public UserRepository(UserDbContext applicationContext)
		{
			this.applicationContext = applicationContext;
		}

		public async Task<bool> CheckLoginAsync(string userName, string hashPassword, CancellationToken cancellationToken)
		{
			try
			{
				return await applicationContext.Users
					.AsNoTracking()
					.Where(u => u.Name == userName)
					.AnyAsync(u => EF.Property<string>(u, "Password") == hashPassword, cancellationToken);
			}
			catch (Exception e)
			{
				throw new ApplicationErrorException("Error while check login user", e);
			}
		}

		public async Task<User> CreateAsync(User item, string hashPassword, CancellationToken cancellationToken)
		{
			try
			{
				applicationContext.Users.Add(item);

				applicationContext.Entry(item).Property("Password").CurrentValue = hashPassword;

				await applicationContext.SaveChangesAsync(cancellationToken);

				return item;
			}
			catch (DbUpdateException dbue)
			{
				if (dbue.InnerException is PostgresException pg && pg.SqlState == PostgresErrorCodes.UniqueViolation)
				{
					throw new BusinessLogicException($"User with same Name \"{item.Name}\" already exist");
				}
				else
				{
					throw new ApplicationErrorException($"Create user error", dbue);
				}
			}
			catch (Exception e)
			{
				throw new ApplicationErrorException("Error while create user", e);
			}
		}

		public async Task<User?> GetByNameAsync(string userName, CancellationToken cancellationToken)
		{
			try
			{
				return await applicationContext.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Name == userName, cancellationToken);
			}
			catch (Exception e)
			{
				throw new ApplicationErrorException("Error while check login user", e);
			}
		}
	}
}
