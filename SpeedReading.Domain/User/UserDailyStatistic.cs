﻿using SpeedReading.Domain.User.OwnedTables;

namespace SpeedReading.Domain.User
{
	public class UserDailyStatistic
	{
		// private static int TaskInPlanCount = 10;

		public int Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime Date { get; set; }
		public List<UserTaskStatistic> CompletedTasks { get; set; } = new();
		/*public List<TrainingTask> AdditionalTasks => PlanedTaskPercent < 100 
			? new() 
			: CompletedTasks.GetRange(10, CompletedTasksCount - 10);*/
		/*public int PlanedTaskPercent => CompletedTasksCount > TaskInPlanCount 
			? 100 
			: CompletedTasksCount / TaskInPlanCount * 100; 
		public int AdditionalTasksCount => CompletedTasksCount > TaskInPlanCount 
			? CompletedTasksCount - TaskInPlanCount 
			: 0;*/
	}
}
