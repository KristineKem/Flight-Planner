using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validation;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : BaseApiController
    {
        private static readonly object lockObject = new object();
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IFlightValidate> _validators;

        public AdminApiController(
                IFlightService flightService,
                IMapper mapper,
                IEnumerable<IFlightValidate> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }
    
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlight(id);
            if(flight == null)
            {
                return NotFound();
            }
           
            return Ok(_mapper.Map<AddFlightRequest>(flight));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            lock(lockObject)
            {
                var flight = _mapper.Map<Flight>(request);

                if (_flightService.DoesFlightAlreadyExist(flight))
                {
                    return Conflict();
                }

                if (!_validators.All(validator => validator.IsValid(flight)))
                    return BadRequest();

                _flightService.Create(flight);

                return Created("", _mapper.Map<AddFlightRequest>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (lockObject)
            {
                var flight = _flightService.GetFullFlight(id);

                if (flight == null)
                {
                    return Ok();
                }

                _flightService.Delete(flight);
                
                return Ok();
            }
        }
    }
}
