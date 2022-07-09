namespace SpeedReading.Application.Dtos.Task
{
	public record TrainingTaskDto : IMapWith<Domain.Task.TrainingTask>
	{
		public Guid Id { get; init; }
		public string Title { get; init; }
		public string Description { get; init; }
		public string ProgramName { get; init; }
		public string Skill { get; init; }
	}

	public record TrainingTaskListDto(IList<TrainingTaskDto> TrainingTasks);
}
