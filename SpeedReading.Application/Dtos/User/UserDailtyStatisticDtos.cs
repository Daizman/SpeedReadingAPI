using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Application.Dtos.User
{
	public record UserDailyTaskStatisticDto(Guid TaskId, string Name, string Type, int CompleteCount, TimeSpan? Time, TimeSpan? RecordTime);

	public record UserDailyStatisticDto(
		int Id,
		Guid UserId,
		DateTime Date,
		List<UserDailyTaskStatisticDto> CompletedTasks,
		List<UserDailyTaskStatisticDto> AdditionalTasks);

	public record AddUserTaskDailyStatisticDto(Guid UserId, Guid TaskId, TaskName TaskName, DateTime Date, TimeSpan? Time);
}
