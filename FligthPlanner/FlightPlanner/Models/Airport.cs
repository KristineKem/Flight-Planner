using System.Text.Json.Serialization;

namespace FlightPlanner.Models
{
    public class Airport
    {        
        public string City { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }

    }
}
