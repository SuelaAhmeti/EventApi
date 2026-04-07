namespace EventApi.Models
{
    public class EventStats
    {
        public int Count { get; set; }
        public double TotalPrice { get; set; }
        public double AveragePrice { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}
