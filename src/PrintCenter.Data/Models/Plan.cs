using System;
using System.Collections.Generic;

namespace PrintCenter.Data.Models
{
    public class Plan : IHasId
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsApproved { get; set; }

        public User Author { get; set; }

        public List<Stream> Streams { get; set; }
    }
}