using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validation;

namespace FlightPlanner.Services.Validations.FlightValidations
{
    public class FlightTimeValidator : IFlightValidate
    {
        public bool IsValid(Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.ArrivalTime) && !string.IsNullOrEmpty(flight?.DepartureTime);
        }
    }
}
