using SpeedReading.Domain.User;

namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class UserTextConfiguration : IEntityTypeConfiguration<UserText>
	{
		public void Configure(EntityTypeBuilder<UserText> builder)
		{
			builder.ToTable(nameof(UserText));
		}
	}
}
