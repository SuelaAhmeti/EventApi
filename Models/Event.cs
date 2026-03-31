using System;

namespace EventApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int OrganizerId { get; set; }
    }
}