using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validation;

namespace FlightPlanner.Services.Validations.FlightValidations
{
    public class FlightValidator : IFlightValidate
    {
        public bool IsValid(Flight? flight)
        {
            return flight != null;
        }
    }
}
