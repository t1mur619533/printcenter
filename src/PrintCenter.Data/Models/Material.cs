using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PrintCenter.Data.Models
{
    public class Material : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Parameter { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public double Count { get; set; }

        public double NormalCount { get; set; }

        public double MinimalCount { get; set; }

        [JsonIgnore]
        public List<MaterialMovement> MaterialMovements { get; set; }
        
        [JsonIgnore]
        public List<MaterialConsumption> MaterialConsumptions { get; set; }
    }
}