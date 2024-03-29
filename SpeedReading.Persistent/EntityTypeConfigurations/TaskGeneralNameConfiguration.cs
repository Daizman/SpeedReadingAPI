﻿namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class TaskGeneralNameConfiguration : IEntityTypeConfiguration<TaskGeneralName>
	{
		public void Configure(EntityTypeBuilder<TaskGeneralName> builder)
		{
			builder.HasKey(general => new { general.ProgramName, general.LanguageId });
			builder.HasIndex(general => general.ProgramName).IsUnique();
			builder.Property(general => general.Title).HasMaxLength(256);
			builder.Property(general => general.Description).HasMaxLength(1024);
		}
	}
}
