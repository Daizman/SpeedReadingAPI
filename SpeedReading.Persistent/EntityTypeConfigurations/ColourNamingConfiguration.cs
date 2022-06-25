namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class ColourNamingConfiguration : IEntityTypeConfiguration<ColourNaming>
	{
		public void Configure(EntityTypeBuilder<ColourNaming> builder)
		{
			builder.HasBaseType(typeof(TrainingTask));
		}
	}
}
