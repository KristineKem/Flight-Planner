using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{    
    [ApiController]
    [Route("api")]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string typehead)
        {
            
            var airports = AirportStorage.SearchAirports(typehead);
            if(airports == null)
            {
                return NotFound();
            }

            return Ok(airports);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(CustomerRequest request)
        {                  
            if(FlightStorage.IsRequestValuesValid(request))
            {
                return BadRequest();
            }

            var flights = FlightStorage.FindFlights(request);
            return Ok(flights);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = FlightStorage.SearchFlightById(id);
            if(flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
