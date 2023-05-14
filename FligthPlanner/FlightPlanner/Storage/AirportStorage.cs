using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public static class AirportStorage
    {
        public static List<Airport> _airports = new List<Airport>();

        public static void Clear()
        {
            _airports.Clear();
        }

        public static List<Airport> SearchAirports(string typehead)
        {
            var foundAirports = _airports.FindAll(a => a.AirportCode.ToLower().Contains(CleanString(typehead))
                                                    || a.City.ToLower().Contains(CleanString(typehead))
                                                    || a.Country.ToLower().Contains(CleanString(typehead)));

            return foundAirports;
        }

        public static string CleanString(string str)
        {
            return str.ToLower().Trim();
        }

        public static bool DoesAirportExist(Airport airport)
        {
            if(_airports.Any(a => a.Country == airport.Country
                                && a.City == airport.City
                                && a.AirportCode == airport.AirportCode))
                return true;

            return false;
        }
      
        public static void AddAirport(Airport airport)
        { 
            if(!DoesAirportExist(airport) && IsAirportValuesValid(airport))
            {
                 _airports.Add(airport);
            }
           
        }

        public static bool IsAirportValuesValid(Airport airport)
        {
            if(string.IsNullOrEmpty(airport.AirportCode)
                || string.IsNullOrEmpty(airport.Country)
                || string.IsNullOrEmpty(airport.Country))
                return false;

            return true;
        }
    }
}
 