using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validation;

namespace FlightPlanner.Services.Validations.FlightValidations
{
    public class AppropriateDateValidator : IFlightValidate
    {
        public bool IsValid(Flight? flight)
        {
            return DateTime.Compare(DateTime.Parse(flight.DepartureTime), DateTime.Parse(flight.ArrivalTime)) < 0;
        }
    }
}
