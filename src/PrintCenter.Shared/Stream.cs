using System.Collections.Generic;

namespace PrintCenter.Shared
{
    public class Stream
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double SizeX { get; set; }

        public double SizeY { get; set; }

        public int PackageSize { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public int PackagesCount { get; set; }

        public string YaltaNumber { get; set; }

        public string YaltaPosition { get; set; }

        public string TechnologyName { get; set; }

        public string CustomerName { get; set; }

        public int RequestNumber { get; set; }

        public int PlanNumber { get; set; }

        public int InvoiceNumber { get; set; }
    }

    public class StreamsEnvelope : Envelope<List<Stream>>
    {
        public StreamsEnvelope(List<Stream> model, int totalCount) : base(model, totalCount)
        {
        }
    }
}
