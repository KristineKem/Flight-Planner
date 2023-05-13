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
            var result = _flights.Any(f => f.From.Country == flight.From.Country
                                        && f.From.City == flight.From.City
                                        && f.From.AirportCode == flight.From.AirportCode
                                        && f.To.Country == flight.To.Country
                                        && f.To.City == flight.To.City
                                        && f.To.AirportCode == flight.To.AirportCode
                                        && f.DepartureTime == flight.DepartureTime
                                        && f.ArrivalTime == flight.ArrivalTime
                                        && f.Carrier == flight.Carrier);
            return result;
        }

        public static bool IsFlightValuesValid(Flight flight)
        {
            if (string.IsNullOrEmpty(flight.Carrier)
                || string.IsNullOrEmpty(flight.ArrivalTime)
                || string.IsNullOrEmpty(flight.DepartureTime)
                || string.IsNullOrEmpty(flight.To.Country)
                || string.IsNullOrEmpty(flight.To.City)
                || string.IsNullOrEmpty(flight.To.AirportCode)
                || string.IsNullOrEmpty(flight.From.Country)
                || string.IsNullOrEmpty(flight.From.City)
                || string.IsNullOrEmpty(flight.From.AirportCode))
                return true;

            return false;
        }

        public static bool IsSameAirportCodes(Flight flight)
        {
            if(flight.To.AirportCode.ToLower().Trim() == flight.From.AirportCode.ToLower().Trim())
                return true;

            return false;
        }

        public static bool IsDateValid(Flight flight)
        {
            if(DateTime.Compare(DateTime.Parse(flight.DepartureTime),
                DateTime.Parse(flight.ArrivalTime)) < 0)
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
            
            var flights = _flights.FindAll(f => f.From.AirportCode == request.From
                                                && f.To.AirportCode == request.To
                                                && f.DepartureTime.Contains(request.DepartureDate));

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
            if(string.IsNullOrEmpty(request.From)
                || string.IsNullOrEmpty(request.To)
                || string.IsNullOrEmpty(request.DepartureDate))
                return false;
            //05 testos pēdējais. :)
            if (request.From.ToLower().Trim() == request.To.ToLower().Trim())
            {
                return false;
            }

            return true;
        } 

        public static Flight SearchFlightById(int id)
        {
            var flight = GetFlight(id);

            return flight;
        }
    }
}
