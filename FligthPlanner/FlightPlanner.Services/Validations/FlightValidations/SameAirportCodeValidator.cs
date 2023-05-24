using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validation;

namespace FlightPlanner.Services.Validations.FlightValidations
{
    public class SameAirportCodeValidator : IFlightValidate
    {
        public bool IsValid(Flight flight)
        {
            return flight?.From?.AirportCode.ToLower().Trim() != flight?.To?.AirportCode.ToLower().Trim();
        }
    }
}
