using System.Collections.Generic;

namespace PrintCenter.Domain.Materials
{
    public class MaterialsEnvelope
    {
        public List<Material> Materials { get; set; }

        public MaterialsEnvelope(List<Material> materials)
        {
            Materials = materials;
        }
    }
}
