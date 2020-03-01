using System.Collections.Generic;

namespace PrintCenter.Data.Models
{
    public class SerialProduction : IHasId
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double SizeX { get; set; }

        public double SizeY { get; set; }

        public int PackageSize { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }
        
        public Technology Technology { get; set; }

        public List<MaterialConsumption> MaterialConsumptions { get; set; }
        
        public SerialProduction()
        {
            MaterialConsumptions = new List<MaterialConsumption>();
        }
    }
}