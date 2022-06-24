using SpeedReading.Domain;

namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class TextConfiguration : IEntityTypeConfiguration<Text>
	{
		public void Configure(EntityTypeBuilder<Text> builder)
		{
			builder.HasKey(text => text.Id);
			builder.HasIndex(text => text.Id).IsUnique();
			builder.Property(text => text.Title).HasMaxLength(256);
			builder.Property(text => text.Description).HasMaxLength(2048);
		}
	}
}
