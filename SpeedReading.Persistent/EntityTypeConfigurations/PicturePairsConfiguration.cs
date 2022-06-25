namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class PicturePairsConfiguration : IEntityTypeConfiguration<PicturePair>
	{
		public void Configure(EntityTypeBuilder<PicturePair> builder)
		{
			builder.HasBaseType(typeof(TrainingTask));
		}
	}
}
