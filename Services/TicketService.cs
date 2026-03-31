using EventApi.Models;
using EventApi.Data;

namespace EventApi.Services
{
    public class TicketService
    {
        public void BuyTicket(Ticket ticket)
        {
            AppDb.Tickets.Add(ticket);
        }
    }
}
