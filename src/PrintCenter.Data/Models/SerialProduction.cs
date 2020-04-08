using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public List<CompositeSerialProductionSerialProduction> CompositeSerialProductionSerialProductions { get; set; }

        [NotMapped]
        public List<CompositeSerialProduction> CompositeSerialProduction => CompositeSerialProductionSerialProductions.Where(production => production.SerialProductionId.Equals(Id)).Select(production => production.CompositeSerialProduction).ToList();

        [JsonIgnore]
        public List<MaterialConsumptionSerialProduction> MaterialConsumptionSerialProductions { get; set; }

        [NotMapped]
        public List<MaterialConsumption> MaterialConsumptions => MaterialConsumptionSerialProductions.Where(production => production.SerialProductionId.Equals(Id)).Select(production => production.MaterialConsumption).ToList();

    }
}