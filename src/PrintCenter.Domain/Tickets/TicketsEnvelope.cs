using System.Collections.Generic;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Tickets
{
    public class TicketsEnvelope
    {
        public List<Ticket> Notifications { get; set; }

        public TicketsEnvelope(List<Ticket> notifications)
        {
            Notifications = notifications;
        }
    }
}
