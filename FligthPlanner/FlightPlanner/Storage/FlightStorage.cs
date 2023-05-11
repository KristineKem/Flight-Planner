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
            if (String.IsNullOrEmpty(flight.Carrier.ToString())
                    && String.IsNullOrEmpty(flight.ArrivalTime.ToString())
                    && String.IsNullOrEmpty(flight.DepartureTime.ToString())
                    && String.IsNullOrEmpty(flight.To.Country.ToString())
                    && String.IsNullOrEmpty(flight.To.City.ToString())
                    && String.IsNullOrEmpty(flight.To.AirportCode.ToString())
                    && String.IsNullOrEmpty(flight.From.Country.ToString())
                    && String.IsNullOrEmpty(flight.From.City.ToString())
                    && String.IsNullOrEmpty(flight.From.AirportCode.ToString()))
                return true;

            return false;
        }
    }
}
