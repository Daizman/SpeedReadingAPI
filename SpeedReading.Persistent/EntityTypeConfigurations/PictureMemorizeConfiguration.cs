namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class PictureMemorizeConfiguration : IEntityTypeConfiguration<PictureMemorize>
	{
		public void Configure(EntityTypeBuilder<PictureMemorize> builder)
		{
			builder.ToTable(nameof(PictureMemorize));
		}
	}
}
