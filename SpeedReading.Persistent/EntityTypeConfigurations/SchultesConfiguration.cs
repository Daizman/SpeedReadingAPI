namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class SchultesConfiguration : IEntityTypeConfiguration<Schulte>
	{
		public void Configure(EntityTypeBuilder<Schulte> builder)
		{
			builder.HasBaseType(typeof(TrainingTask));
		}
	}
}
