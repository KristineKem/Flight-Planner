using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;

        public static Flight GetFlight(int id)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
        }

        public static Flight AddFlight(Flight flight)
        {           
            flight.Id = _id++;
            _flights.Add(flight);
            return flight;           
        }

        public static bool DoesFlightAlreadyExist(Flight flight) 
        {
            return _flights.Contains(flight);
        }

        public static bool IsFlightValuesValid(Flight flight)
        {
            if (string.IsNullOrEmpty(flight.Carrier.ToString())
                    && string.IsNullOrEmpty(flight.ArrivalTime.ToString())
                    && string.IsNullOrEmpty(flight.DepartureTime.ToString())
                    && string.IsNullOrEmpty(flight.To.Country.ToString())
                    && string.IsNullOrEmpty(flight.To.City.ToString())
                    && string.IsNullOrEmpty(flight.To.AirportCode.ToString())
                    && string.IsNullOrEmpty(flight.From.Country.ToString())
                    && string.IsNullOrEmpty(flight.From.City.ToString())
                    && string.IsNullOrEmpty(flight.From.AirportCode.ToString()))
                return true;

            return false;
        }
    }
}
