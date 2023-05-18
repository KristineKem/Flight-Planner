using FlightPlanner.Models;
using FlightPlanner.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : BaseApiController
    {
        private static readonly object lockObject = new object();
        

        public AdminApiController(FlightPlannerDbContext context) : base(context) { }
    
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights.Include(f => f.From)
                                            .Include(f => f.To)
                                            .SingleOrDefault(f => f.Id == id);
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
                if (DoesFlightAlreadyExist(flight))
                {
                    return Conflict();
                }

                if (!RequestDataValidation.IsFlightValuesValid(flight)
                    || RequestDataValidation.IsSameAirportCodes(flight)
                    || !RequestDataValidation.IsFlightDatesCorrect(flight))
                    return BadRequest();

                _context.Flights.Add(flight);
                _context.Airports.Add(flight.To);
                _context.Airports.Add(flight.From);
;               _context.SaveChanges();
                return Created("", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (lockObject)
            {
                var flight = _context.Flights.SingleOrDefault(f => f.Id == id);

                if (flight == null)
                {
                    return Ok();
                }

                _context.Flights.Remove(flight);
                _context.SaveChanges();

                return Ok();
            }
        }
    }
}
