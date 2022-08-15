using SpeedReading.Domain.Localization;

namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class LanguageConfiguration : IEntityTypeConfiguration<Language>
	{
		public void Configure(EntityTypeBuilder<Language> builder)
		{
			builder.HasKey(lang => lang.Id);
			builder.HasIndex(lang => lang.Id).IsUnique();
		}
	}
}
