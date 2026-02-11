using CurrencyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyService.Infrastructure.Configurations
{
	public sealed class CurrencyUserConfiguration : IEntityTypeConfiguration<CurrencyUser>
	{
		public void Configure(EntityTypeBuilder<CurrencyUser> builder)
		{
			builder.ToTable("currency_users");

			builder.HasKey(q => q.Id);

			builder.Property(q => q.Id)
				.HasColumnName("id");

			builder.Property(q => q.UserId)
				.HasColumnName("user_id");

			builder.Property(q => q.CurrencyId)
				.HasColumnName("currency_id");

			builder.HasOne(q => q.Currency)
				.WithMany()
				.HasForeignKey(q => q.CurrencyId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(q => new { q.UserId, q.CurrencyId })
				.IsUnique();
		}
	}
}
