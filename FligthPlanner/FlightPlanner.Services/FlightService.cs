using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlight(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public List<Flight> FindFlights(CustomerRequest request)
        {
            return _context.Flights.Where(f => f.DepartureTime.Contains(request.DepartureDate)
                                               && f.From.AirportCode.ToLower().Contains(request.From.ToLower())
                                               && f.To.AirportCode.ToLower().Contains(request.To.ToLower())).ToList();
        }

        public bool DoesFlightAlreadyExist(Flight flight)
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

        public bool IsRequestValuesValid(CustomerRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureDate)
                || !string.IsNullOrEmpty(request.From)
                || !string.IsNullOrEmpty(request.To);
        }
    }
}
