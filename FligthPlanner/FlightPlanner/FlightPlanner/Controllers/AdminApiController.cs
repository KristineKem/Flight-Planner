using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        [Route("fligths/{id}")]
        public IActionResult GetFlight(int id)
        {
            return Ok("returnning flights");
        }
    }
}
