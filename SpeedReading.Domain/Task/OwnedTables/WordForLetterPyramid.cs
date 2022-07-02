using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace SpeedReading.Domain.Task.OwnedTables
{
	[Owned]
	public class WordForLetterPyramid
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public string Word { get; set; }
	}
}
