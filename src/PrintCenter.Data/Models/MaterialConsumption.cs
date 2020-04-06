namespace PrintCenter.Data.Models
{
    public class MaterialConsumption : IHasId
    {
        public int Id { get; set; }
        
        public double Rate { get; set; }

        public Material Material { get; set; }
    }
}