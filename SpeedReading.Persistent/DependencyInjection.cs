using Microsoft.Extensions.DependencyInjection;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Common.Interfaces;
using SpeedReading.Application.Common.Models;

namespace SpeedReading.Persistent
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistent(this IServiceCollection services, PostgreSqlDbSettings settings)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseNpgsql(settings.ConnectionString, npgsqlOptions =>
				{
					npgsqlOptions.MigrationsAssembly("SpeedReading.Api");
				});
			});
			
			services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
			services.AddScoped<IJwtUtils, JwtUtils>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserStatisticService, UserStatisticService>();
			return services;
		}
	}
}
