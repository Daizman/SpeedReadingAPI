namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class LetterPyramidsConfiguration : IEntityTypeConfiguration<LetterPyramids>
	{
		public void Configure(EntityTypeBuilder<LetterPyramids> builder)
		{
			builder.HasBaseType(typeof(TrainingTask));
			builder.ToTable(nameof(LetterPyramids));
		}
	}
}
