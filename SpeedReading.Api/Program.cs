using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using SpeedReading.Api;
using SpeedReading.Api.Middlewares.Extensions;
using SpeedReading.Application.Common.Interfaces;
using SpeedReading.Application.Common.Mapping;
using SpeedReading.Application.Common.Models;
using SpeedReading.Persistent;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var postgreSettings = builder.Configuration.GetSection(nameof(PostgreSqlDbSettings)).Get<PostgreSqlDbSettings>();

// Add services to the container.
builder.Services.AddAutoMapper(options =>
{
	options.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
	options.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});
builder.Services.AddPersistent(postgreSettings);

builder.Services.AddCors(config => 
{
	config.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
		policy.AllowAnyOrigin();
	});
});

builder.Services.AddControllers(options => 
{
	options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddVersionedApiExplorer(options =>
{
	options.GroupNameFormat = "'v'VVV";
});
builder.Services.AddSwaggerGen(options => 
{
	string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	options.IncludeXmlComments(xmlPath);
});
builder.Services.AddApiVersioning();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddHealthChecks()
	.AddNpgSql(
		postgreSettings.ConnectionString,
		name: "postgreDb",
		timeout: TimeSpan.FromSeconds(5),
		tags: new[] { "ready" });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var serviceProvider = scope.ServiceProvider;
	var apiDescriptionProvider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{

		foreach (var description in apiDescriptionProvider.ApiVersionDescriptions)
		{
			options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
				description.GroupName.ToUpperInvariant());
			// swagger в корневом коталоге приложения
			options.RoutePrefix = string.Empty;
		}
	});

	if (app.Environment.IsDevelopment())
	{
		app.UseHttpsRedirection();
	}
	try
	{
		var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
		await DbInitializer.InitializeAsync(context);
	}
	catch (Exception e)
	{
		
	}
}

app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAll");  // toDo: потом переделать

app.UseCustomExceptionHandler();
app.UseJwt();
app.UseApiVersioning();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();

	endpoints.MapHealthChecks("/health/ready", new()
	{
		Predicate = check => check.Tags.Contains("ready"),
		ResponseWriter = async(context, report) => 
		{
			var result = JsonSerializer.Serialize(
				new 
				{
					status = report.Status.ToString(),
					checks = report.Entries.Select(entry => new
					{
						name = entry.Key,
						status = entry.Value.Status.ToString(),
						excetion = entry.Value.Exception is not null
								 ? entry.Value.Exception.Message
								 : "none",
						duration = entry.Value.Duration.ToString()
					})
				});

			context.Response.ContentType = MediaTypeNames.Application.Json;
			await context.Response.WriteAsync(result);
		}
	});

	endpoints.MapHealthChecks("/health/alive", new()
	{
		Predicate = _ => false
	});
});

app.Run();
