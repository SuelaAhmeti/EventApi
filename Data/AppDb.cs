using System.Collections.Generic;
using EventApi.Models;

namespace EventApi.Data
{
    public static class AppDb
    {
        public static List<User> Users = new List<User>();
        public static List<Category> Categories = new List<Category>();
        public static List<Event> Events = new List<Event>();
        public static List<Ticket> Tickets = new List<Ticket>();
    }
}
