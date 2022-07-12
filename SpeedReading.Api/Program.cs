using SpeedReading.Api.Middlewares.Extensions;
using SpeedReading.Application.Common.Models;
using SpeedReading.Persistent;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var postgreSettings = builder.Configuration.GetSection(nameof(PostgreSqlDbSettings)).Get<PostgreSqlDbSettings>();

// Add services to the container.
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
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
	string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	options.IncludeXmlComments(xmlPath);
});
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
	try
	{
		var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
		await DbInitializer.InitializeAsync(context);
	}
	catch (Exception e)
	{
		
	}
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options => 
	{
		options.RoutePrefix = string.Empty;
		options.SwaggerEndpoint("swagger/v1/swagger.json", "SpeadReading.Api");
	});
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAll");  // toDo: потом переделать

app.UseCustomExceptionHandler();
app.UseJwt();

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
