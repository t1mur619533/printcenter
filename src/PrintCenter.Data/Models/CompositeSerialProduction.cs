using System.Collections.Generic;

namespace PrintCenter.Data.Models
{
    public class CompositeSerialProduction : IHasId
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int PackageSize { get; set; }

        public List<SerialProduction> SerialProductions { get; set; }

        public CompositeSerialProduction()
        {
            SerialProductions = new List<SerialProduction>();
        }
    }
}