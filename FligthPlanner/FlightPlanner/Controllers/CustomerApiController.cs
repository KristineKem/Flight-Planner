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
        public IActionResult SearchAirports(string search)
        {
            
            var airports = AirportStorage.SearchAirports(search);
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
            //request ir valid.. atgriež true.. tapēc izpildās if un saka Bad request. :D pieņemam tikai sliktos pieprasījumus. :D
            if (!FlightStorage.IsRequestValuesValid(request))
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
