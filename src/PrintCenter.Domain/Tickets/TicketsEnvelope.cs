using System.Collections.Generic;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Tickets
{
    public class TicketsEnvelope
    {
        public List<Ticket> Tickets { get; set; }

        public TicketsEnvelope(List<Ticket> tickets)
        {
            Tickets = tickets;
        }
    }
}
