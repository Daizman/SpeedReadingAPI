using SpeedReading.Application.Common.Interfaces;
using SpeedReading.Domain;
using SpeedReading.Domain.User;
using SpeedReading.Persistent.EntityTypeConfigurations;

namespace SpeedReading.Persistent
{
	public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserDailyStatistic> UsersDailyStatistics { get; set; }
		public DbSet<Text> Texts { get; set; }
		public DbSet<UserText> UsersTexts { get; set; }
		public DbSet<TaskGeneralName> TasksGeneralNames { get; set; }
		public DbSet<TrainingTask> TrainingTasks { get; set; }
		public DbSet<ColourNaming> ColourNamings { get; set; }
		public DbSet<LetterPyramids> LetterPyramids { get; set; }
		public DbSet<PictureMemorize> PicutreMemorizes { get; set; }
		public DbSet<PicturePair> PicturePairs { get; set; }
		public DbSet<Schulte> Schultes { get; set; }
		public DbSet<TaskWithText> TasksWithTexts { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new UserDailyStatisticConfiguration());
			builder.ApplyConfiguration(new TextConfiguration());
			builder.ApplyConfiguration(new UserTextConfiguration());
			builder.ApplyConfiguration(new TaskGeneralNameConfiguration());
			builder.ApplyConfiguration(new TrainingTaskConfiguration());
			builder.ApplyConfiguration(new ColourNamingConfiguration());
			builder.ApplyConfiguration(new LetterPyramidsConfiguration());
			builder.ApplyConfiguration(new PictureMemorizeConfiguration());
			builder.ApplyConfiguration(new PicturePairsConfiguration());
			builder.ApplyConfiguration(new SchultesConfiguration());
			builder.ApplyConfiguration(new TaskWithTextConfiguration());
			base.OnModelCreating(builder);
		}
	}
}
