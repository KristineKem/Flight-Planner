using FlightPlanner.Models;
using FlightPlanner.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{    
    [ApiController]
    [Route("api")]
    public class CustomerApiController : BaseApiController
    {
        public CustomerApiController(FlightPlannerDbContext context) : base(context) { }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = _context.Airports.Where(a => a.AirportCode.ToLower().Contains(search.ToLower().Trim())
                                                        || a.City.ToLower().Contains(search.ToLower().Trim())
                                                        || a.Country.ToLower().Contains(search.ToLower().Trim()))
                .ToList();

            if (airports == null)
            {
                return NotFound();
            }

            return Ok(airports);

        }
        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(CustomerRequest request)
        {
            if (!RequestDataValidation.IsRequestValuesValid(request))
            {
                return BadRequest();
            }

            if (request.From == request.To)
            {
                return BadRequest();
            }

            var pageResult = new PageResult();

            var flights = _context.Flights.Where(f => f.DepartureTime.Contains(request.DepartureDate) 
                                                      && f.From.AirportCode.ToLower().Contains(request.From.ToLower()) 
                                                      && f.To.AirportCode.ToLower().Contains(request.To.ToLower())).ToList();

            foreach (var flight in flights)
            {
                pageResult.Items.Add(flight);
            }

            pageResult.TotalItems = flights.Count;
            pageResult.Page = 0;

            return Ok(pageResult);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _context.Flights.Include(f => f.From)
                                        .Include(f => f.To)
                                        .SingleOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
