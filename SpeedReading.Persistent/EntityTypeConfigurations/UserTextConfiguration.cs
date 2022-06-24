using SpeedReading.Domain;
using SpeedReading.Domain.User;

namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class UserTextConfiguration : IEntityTypeConfiguration<UserText>
	{
		public void Configure(EntityTypeBuilder<UserText> builder)
		{
			builder.HasBaseType(typeof(Text));
		}
	}
}
