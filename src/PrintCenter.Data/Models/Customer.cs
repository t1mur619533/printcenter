using System.Collections.Generic;

namespace PrintCenter.Data.Models
{
    public class Customer : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Stream> Streams { get; set; }
    }
}