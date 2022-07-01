using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpeedReading.Application.Common.Implementation;
using SpeedReading.Application.Common.Interfaces;

namespace SpeedReading.Persistent
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistent(this IServiceCollection services, IConfiguration configuration)
		{
			string connection = configuration.GetConnectionString("SpeedReadingAPI");
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseNpgsql(connection, npgsqlOptions =>
				{
					npgsqlOptions.MigrationsAssembly("SpeedReading.Api");
				});
			});
			
			services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
			services.AddScoped<IJwtUtils, JwtUtils>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			return services;
		}
	}
}
