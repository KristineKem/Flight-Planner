using FlightPlanner.Models;

namespace FlightPlanner.Validation
{
    public static class RequestDataValidation
    {
        public static bool IsFlightValuesValid(Flight flight)
        {
            if (string.IsNullOrEmpty(flight.ArrivalTime)
                || string.IsNullOrEmpty(flight.DepartureTime)
                || string.IsNullOrEmpty(flight.Carrier)
                || !IsAirportValuesValid(flight.To)
                || !IsAirportValuesValid(flight.From))
                return false;

            return true;
        }

        public static bool IsAirportValuesValid(Airport airport)
        {
            if(string.IsNullOrEmpty(airport.AirportCode)
               || string.IsNullOrEmpty(airport.City)
               || string.IsNullOrEmpty(airport.Country))
                return false;

            return true;
        }

        public static bool IsSameAirportCodes(Flight flight)
        {
            if(flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
                return true;

            return false;
        }

        public static bool IsFlightDatesCorrect(Flight flight)
        {
            if(DateTime.Compare(DateTime.Parse(flight.DepartureTime), DateTime.Parse(flight.ArrivalTime))  < 0) 
                return true;

            return false;
        }
    }
}
