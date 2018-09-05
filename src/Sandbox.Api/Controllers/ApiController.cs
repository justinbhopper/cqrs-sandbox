using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Domain;
using Sandbox.Domain.Models;

namespace Sandbox.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public abstract class ApiController : ControllerBase
	{
	}
}
