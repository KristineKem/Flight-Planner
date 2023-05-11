using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(Airport airport)
        {
            AirportStorage.SearchAirport(airport);
            return Ok();
        }

    }
}
