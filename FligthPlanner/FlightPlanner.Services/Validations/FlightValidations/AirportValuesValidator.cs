using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validation;

namespace FlightPlanner.Services.Validations.FlightValidations
{
    public class AirportValuesValidator : IFlightValidate
    {
        public bool IsValid(Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.From?.AirportCode)
                   && !string.IsNullOrEmpty(flight?.From?.City)
                   && !string.IsNullOrEmpty(flight?.From?.Country)
                   && !string.IsNullOrEmpty(flight?.To?.AirportCode)
                   && !string.IsNullOrEmpty(flight?.To?.City)
                   && !string.IsNullOrEmpty(flight?.To?.Country);
        }
    }
}
