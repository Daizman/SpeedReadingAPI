namespace SpeedReading.Domain
{
	public class Text
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; } = string.Empty;
		public string Content { get; set; }
		public int LanguageId { get; set; }
	}
}
