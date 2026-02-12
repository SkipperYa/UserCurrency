using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Configurations
{
	/// <summary>
	/// Настройка конфигурации пользователя
	/// </summary>
	public sealed class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("users");

			builder.HasKey(q => q.Id);

			builder.Property(q => q.Id)
				.HasColumnName("id");

			builder.Property(q => q.Name)
				.HasColumnName("name")
				.IsRequired()
				.HasMaxLength(256);

			builder.HasIndex(q => q.Name)
				.IsUnique();

			builder.Property<string>("Password")
				.HasColumnName("password_hash")
				.IsRequired();
		}
	}
}
