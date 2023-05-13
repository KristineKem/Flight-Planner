using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object lockObject = new object();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if(flight == null)
            {
                return NotFound();
            }
           
            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
            lock(lockObject)
            {
                
                if(FlightStorage.DoesFlightAlreadyExist(flight) == true)
                {
                   return Conflict();
                }

                if(FlightStorage.IsFlightValuesValid(flight))
                {
                    return BadRequest();
                }

                if(FlightStorage.IsSameAirportCodes(flight))
                {
                    return BadRequest();
                }

                if(FlightStorage.IsDateValid(flight) == false)
                {
                    return BadRequest();
                }
                
                FlightStorage.AddFlight(flight);
                AirportStorage.AddAirport(flight.To);
                AirportStorage.AddAirport(flight.From);
                return Created("", flight);
            }            
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (lockObject)
            {
                var flight = FlightStorage.RemoveFlight(id);
                if (flight == null)
                {
                    return Ok();
                }

                return Ok();
            }
        }
    }
}
