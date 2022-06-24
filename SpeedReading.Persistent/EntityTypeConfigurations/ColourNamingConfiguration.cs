namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class ColourNamingConfiguration : IEntityTypeConfiguration<ColourNaming>
	{
		public void Configure(EntityTypeBuilder<ColourNaming> builder)
		{
			builder.HasBaseType(typeof(TrainingTask));
			builder.HasKey(cn => cn.Id);
			builder.HasIndex(cn => cn.Id).IsUnique();
		}
	}
}
