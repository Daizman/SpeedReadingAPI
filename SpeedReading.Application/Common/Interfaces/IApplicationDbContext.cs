using SpeedReading.Domain;
using SpeedReading.Domain.Localization;
using SpeedReading.Domain.Task;
using SpeedReading.Domain.User;

namespace SpeedReading.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<UserDailyStatistic> UsersDailyStatistics { get; set; }
		DbSet<Text> Texts { get; set; }
		DbSet<UserText> UsersTexts { get; set; }
		DbSet<TaskGeneralName> TasksGeneralNames { get; set; }
		DbSet<ColourNaming> ColourNamings { get; set; }
		DbSet<LetterPyramids> LetterPyramids { get; set; }
		DbSet<PictureMemorize> PictureMemorizes { get; set; }
		DbSet<PicturePair> PicturePairs { get; set; }
		DbSet<Schulte> Schultes { get; set; }
		DbSet<TaskWithText> TasksWithTexts { get; set; }
		DbSet<Language> Languages { get; set; }
		DbSet<TextResource> TextResources { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
