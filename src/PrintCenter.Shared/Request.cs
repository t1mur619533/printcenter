using System;
using System.Collections.Generic;

namespace PrintCenter.Shared
{
    public class Request
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsApproved { get; set; }

        public User Author { get; set; }

        public List<Stream> Streams { get; set; }
    }

    public class RequestsEnvelope : Envelope<Request> {
        public RequestsEnvelope(Request model, int totalCount) : base(model, totalCount)
        {
        }
    }
}