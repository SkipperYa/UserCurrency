using CurrencyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyService.Infrastructure.Configurations
{
	/// <summary>
	/// Настройка конфигурации валюты
	/// </summary>
	public sealed class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
	{
		public void Configure(EntityTypeBuilder<Currency> builder)
		{
			builder.ToTable("currencies");

			builder.HasKey(q => q.Id);

			builder.Property(q => q.Id)
				.HasColumnName("id");

			builder.Property(q => q.Name)
				.HasColumnName("name")
				.IsRequired()
				.HasMaxLength(256);

			builder.Property(q => q.Rate)
				.HasColumnName("rate")
				.IsRequired();

			builder.HasIndex(q => q.Name)
				.IsUnique();
		}
	}
}
