namespace FlightPlanner.Models
{
    public class CustomerRequest
    {
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string DepartureDate { get; set; }
    }
}
