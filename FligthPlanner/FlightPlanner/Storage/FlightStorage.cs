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
            var result = _flights.Contains(flight);
            return result;
        }

        public static bool IsFlightValuesValid(Flight flight)
        {
            if (string.IsNullOrEmpty(flight.Carrier.ToString())
                    || string.IsNullOrEmpty(flight.ArrivalTime.ToString())
                    || string.IsNullOrEmpty(flight.DepartureTime.ToString())
                    || string.IsNullOrEmpty(flight.To.Country.ToString())
                    || string.IsNullOrEmpty(flight.To.City.ToString())
                    || string.IsNullOrEmpty(flight.To.AirportCode.ToString())
                    || string.IsNullOrEmpty(flight.From.Country.ToString())
                    || string.IsNullOrEmpty(flight.From.City.ToString())
                    || string.IsNullOrEmpty(flight.From.AirportCode.ToString()))
                return true;

            return false;
        }

        public static bool IsSameAirportCodes(Flight flight)
        {
            if(flight.To.AirportCode.ToString().ToLower() == flight.From.AirportCode.ToString().ToLower()
                    || flight.To.Country.ToString().ToLower() == flight.From.Country.ToString().ToLower()
                    || flight.To.City.ToString().ToLower() == flight.From.City.ToString().ToLower())
                return true;

            return false;
        }

        public static bool IsDateValid(Flight flight)
        {
            if(DateTime.Compare(DateTime.Parse(flight.DepartureTime.ToString()),
                DateTime.Parse(flight.ArrivalTime.ToString())) < 0)
                return true;
            return false;
        }

        public static bool RemoveFlight(int id)
        {
            var flightToRemove = _flights.Find(f => f.Id == id);
            return _flights.Remove(flightToRemove);
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 1;
        }

        public static PageResult FindFlights(CustomerRequest request)
        {
            var pageResult = new PageResult();
            
            var flights = _flights.FindAll(f => f.From == request.From
                                                && f.To == request.To
                                                && f.DepartureTime == request.DepartureDate);

            foreach(var flight in flights)
            {
                pageResult.Items.Add(flight);
            }

            pageResult.TotalItems = _flights.Count;
            pageResult.Page = 0;

            return pageResult;
        }

        public static bool IsRequestValuesValid (CustomerRequest request)
        {
            if(string.IsNullOrEmpty(request.From.Country.ToString())
                || string.IsNullOrEmpty(request.From.City.ToString())
                || string.IsNullOrEmpty(request.From.AirportCode.ToString())
                || string.IsNullOrEmpty(request.To.Country.ToString())
                || string.IsNullOrEmpty(request.To.City.ToString())
                || string.IsNullOrEmpty(request.To.AirportCode.ToString())
                || string.IsNullOrEmpty(request.DepartureDate.ToString())
                )
                return false;
            return true;
        }

        

        public static Flight SearchFlightById(int id)
        {
            var flight = GetFlight(id);

            return flight;
        }
    }
}
