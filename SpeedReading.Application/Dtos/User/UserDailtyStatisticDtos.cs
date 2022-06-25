using SpeedReading.Domain.Task;

namespace SpeedReading.Application.Dtos.User
{
	public record UserDailyStatisticDto(
		Guid Id,
		Guid UserId,
		DateTime Date,
		List<TrainingTask> CompletedTasks,
		List<TrainingTask> AdditionalTasks,
		int CompletedTasksCount,
		int PlanedTaskPercent,
		int AdditionalTasksCount);
}
