using SpeedReading.Domain.User;

namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class UserDailyStatisticConfiguration : IEntityTypeConfiguration<UserDailyStatistic>
	{
		public void Configure(EntityTypeBuilder<UserDailyStatistic> builder)
		{
			builder.HasKey(stat => stat.Id);
			builder.HasIndex(stat => stat.Id).IsUnique();
		}
	}
}
