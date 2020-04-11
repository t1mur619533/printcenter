using System.Collections.Generic;

namespace PrintCenter.Domain.Materials
{
    public class Material
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
    }
    
    public class MaterialEnvelope
    {
        public Material Material { get; set; }

        public MaterialEnvelope(Material material)
        {
            Material = material;
        }
    }
    
    public class MaterialsEnvelope
    {
        public List<Material> Materials { get; set; }

        public MaterialsEnvelope(List<Material> materials)
        {
            Materials = materials;
        }
    }
}
