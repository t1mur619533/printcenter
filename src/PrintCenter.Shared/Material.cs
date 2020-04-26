using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrintCenter.Shared
{
    public class Material
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public double Parameter { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public double Count { get; set; }

        public double NormalCount { get; set; }

        public double MinimalCount { get; set; }
    }
    
    public class MaterialsEnvelope : Envelope<List<Material>>
    {
        public MaterialsEnvelope(List<Material> model, int totalCount) : base(model, totalCount)
        {
        }
    }
}
