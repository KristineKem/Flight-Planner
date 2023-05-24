using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validation;

namespace FlightPlanner.Services.Validations.FlightValidations
{
    public class AirportValidator : IFlightValidate
    {
        public bool IsValid(Flight flight)
        {
            return flight?.From != null && flight?.To != null;
        }
    }
}
