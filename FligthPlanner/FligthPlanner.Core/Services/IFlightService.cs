using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlight(int id);

        List<Flight> FindFlights(CustomerRequest request);

        bool DoesFlightAlreadyExist(Flight flight);

        bool IsRequestValuesValid(CustomerRequest request);
    }
}
