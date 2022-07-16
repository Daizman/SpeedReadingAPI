using Microsoft.AspNetCore.Mvc;
using SpeedReading.Api.Attributes.Auth;

namespace SpeedReading.Api.Controllers
{
	[ApiVersionNeutral]
	[Authorize]
	[ApiController]
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	public class CommonController : ControllerBase { }
}
