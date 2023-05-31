using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class ClearController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClearController(IDbService dbService)
        {
           _dbService = dbService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _dbService.Clear<Flight>();
            _dbService.Clear<Airport>();

            return Ok();
        }
    }
}
