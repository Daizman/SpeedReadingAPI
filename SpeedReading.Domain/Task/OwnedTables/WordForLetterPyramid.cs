using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace SpeedReading.Domain.Task.OwnedTables
{
	[Owned]
	public class WordForLetterPyramid
	{
		[JsonIgnore]
		public int Id { get; set; }
		public string Word { get; set; }

		public int LanguageId { get; set; }
	}
}
