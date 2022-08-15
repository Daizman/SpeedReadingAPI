using SpeedReading.Domain.Task.Enums;

namespace SpeedReading.Domain.Task
{
	public class TaskGeneralName
	{
		public TaskName ProgramName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int LanguageId { get; set; }
	}
}
