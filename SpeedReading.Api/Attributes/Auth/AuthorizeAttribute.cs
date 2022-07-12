using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpeedReading.Application.Dtos.User;

namespace SpeedReading.Api.Attributes.Auth
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
			if (allowAnonymous)
			{
				return;
			}

			context.HttpContext.Items.TryGetValue("UserDto", out object? testUserObject);

			if (testUserObject?.GetType().GetProperty("Result")?.GetValue(testUserObject) is not UserDto)
			{
				context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
			}
		}
	}
}
