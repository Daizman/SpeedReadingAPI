﻿namespace SpeedReading.Persistent.EntityTypeConfigurations
{
	public class TaskWithTextConfiguration : IEntityTypeConfiguration<TaskWithText>
	{
		public void Configure(EntityTypeBuilder<TaskWithText> builder)
		{
			builder.ToTable(nameof(TaskWithText));
		}
	}
}
