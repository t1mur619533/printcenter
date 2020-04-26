using System;
using System.Collections.Generic;

namespace PrintCenter.Shared
{
    public class Ticket
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class TicketsEnvelope : Envelope<List<Ticket>>
    {
        public TicketsEnvelope(List<Ticket> model, int totalCount) : base(model, totalCount)
        {
        }
    }
}