using FlightPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected FlightPlannerDbContext _context;

        protected BaseApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        protected bool DoesFlightAlreadyExist(Flight flight)
        {
            return _context.Flights.Any(f => f.From.Country == flight.From.Country
                                             && f.From.City == flight.From.City
                                             && f.From.AirportCode == flight.From.AirportCode
                                             && f.To.Country == flight.To.Country
                                             && f.To.City == flight.To.City
                                             && f.To.AirportCode == flight.To.AirportCode
                                             && f.DepartureTime == flight.DepartureTime
                                             && f.ArrivalTime == flight.ArrivalTime
                                             && f.Carrier == flight.Carrier);
        }
    }
}
