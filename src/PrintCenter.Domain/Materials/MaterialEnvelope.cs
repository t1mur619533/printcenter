namespace PrintCenter.Domain.Materials
{
    public class MaterialEnvelope
    {
        public Material Material { get; set; }

        public MaterialEnvelope(Material material)
        {
            Material = material;
        }
    }
}
