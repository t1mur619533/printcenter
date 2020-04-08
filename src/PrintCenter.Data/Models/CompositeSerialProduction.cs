using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace PrintCenter.Data.Models
{
    public class CompositeSerialProduction : IHasId
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int PackageSize { get; set; }

        [JsonIgnore]
        public List<CompositeSerialProductionSerialProduction> CompositeSerialProductionSerialProductions { get; set; }

        [NotMapped]
        public List<SerialProduction> SerialProductions => CompositeSerialProductionSerialProductions.Where(production => production.CompositeSerialProductionId.Equals(Id)).Select(production => production.SerialProduction).ToList();
    }
}