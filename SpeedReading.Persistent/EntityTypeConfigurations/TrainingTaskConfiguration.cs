namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class TrainingTaskConfiguration : IEntityTypeConfiguration<TrainingTask>
	{
		public void Configure(EntityTypeBuilder<TrainingTask> builder)
		{
			builder.HasKey(task => task.Id);
			builder.HasIndex(task => task.Id).IsUnique();
		}
	}
}
