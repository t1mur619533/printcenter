using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace PrintCenter.Shared
{
    public class Invoice
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsApproved { get; set; }

        public string Author { get; set; }

        [CanBeNull]
        public List<Stream> Streams { get; set; }
    }

    public class InvoicesEnvelope : Envelope<Invoice>
    {
        public InvoicesEnvelope(Invoice model, int totalCount) : base(model, totalCount)
        {
        }
    }
}