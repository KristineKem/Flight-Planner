using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validation
{
    public interface IFlightValidate
    {
        bool IsValid(Flight flight);
    }
}
