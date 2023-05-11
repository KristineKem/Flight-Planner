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

        public static Airport SearchAirport(Airport airport)
        {
            var foundAirport = _airports.Find(a => a.AirportCode.ToString().ToLower().Trim() == airport.AirportCode.ToString().ToLower().Trim()
            && a.Country.ToString().ToLower().Trim() == airport.Country.ToString().ToLower().Trim()
            && a.City.ToString().ToLower().Trim() == airport.City.ToString().ToLower().Trim());

            return foundAirport;
        }
    }
}
