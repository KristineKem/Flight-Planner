﻿using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Airport> SearchAirports(string search)
        {
            return _context.Airports.Where(a => a.AirportCode.ToLower().Contains(search.ToLower().Trim())
                                         || a.City.ToLower().Contains(search.ToLower().Trim())
                                         || a.Country.ToLower().Contains(search.ToLower().Trim())).ToList();
        }

    }
}
