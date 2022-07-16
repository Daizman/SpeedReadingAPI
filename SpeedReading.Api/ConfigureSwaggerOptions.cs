using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SpeedReading.Api
{
	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
		{
			_provider = provider;
		}

		public void Configure(SwaggerGenOptions options)
		{
			foreach (var description in _provider.ApiVersionDescriptions)
			{
				var apiVersion = description.ApiVersion.ToString();

				options.SwaggerDoc(description.GroupName,
					new()
					{
						Version = apiVersion,
						Title = $"Speed reading API {apiVersion}",
						Description = "Auth",
						Contact = new()
						{
							Name = string.Empty,
							Email = string.Empty
						},
						License = new()
						{
							Name = "Open"
						}
					});

				options.AddSecurityDefinition($"AuthToken {apiVersion}",
					new()
					{
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.Http,
						BearerFormat = "JWT",
						Scheme = "bearer",
						Name = "Authorization",
						Description = "Authorization token"
					});
				options.AddSecurityRequirement(new()
				{
					{
						new()
						{
							Reference = new()
							{
								Type = ReferenceType.SecurityScheme,
								Id = $"AuthToken {apiVersion}"
							}
						},
						new string[] { }
					}
				});
				options.CustomOperationIds(apiDescription =>
					apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
			}
		}
	}
}
