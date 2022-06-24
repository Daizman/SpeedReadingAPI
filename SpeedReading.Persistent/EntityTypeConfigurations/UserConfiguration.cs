using SpeedReading.Domain.User;

namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(user => user.Id);
			builder.HasIndex(user => user.Id).IsUnique();
			builder.Property(user => user.Login).HasMaxLength(128);
			builder.Property(user => user.FirstName).HasMaxLength(256);
			builder.Property(user => user.LastName).HasMaxLength(256);
		}
	}
}
