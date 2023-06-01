using FlightPlanner.Models;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace FlightPlanner.Controllers
{    
    [ApiController]
    [Route("api")]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        public CustomerApiController(
            IFlightService flightService,
            IAirportService airportService,
            IMapper mapper)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = _airportService.SearchAirports(search);
                
            if (airports == null)
            {
                return NotFound();
            }

            var mappedAirports = _mapper.Map<AddAirportRequest[]>(airports);

            return Ok(mappedAirports);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(CustomerRequest request)
        {
            if (!_flightService.IsRequestValuesValid(request) 
                || request.From == request.To)
            {
                return BadRequest();
            }

            var pageResult = new PageResult();

            var flights = _flightService.FindFlights(request);

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
            var flight = _flightService.GetFullFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            var mappedFlight = _mapper.Map<AddFlightRequest>(flight);

            return Ok(mappedFlight);
        }
    }
}
