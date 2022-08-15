using SpeedReading.Domain.Localization;

namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class TextResourceConfiguration : IEntityTypeConfiguration<TextResource>
	{
		public void Configure(EntityTypeBuilder<TextResource> builder)
		{
			builder.HasKey(textRes => new { textRes.Key, textRes.LanguageId });
		}
	}
}
