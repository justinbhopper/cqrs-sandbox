using Microsoft.AspNetCore.Mvc;

namespace Sandbox.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public abstract class ApiController : ControllerBase
	{
	}
}
