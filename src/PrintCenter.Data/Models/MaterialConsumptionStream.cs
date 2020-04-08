namespace PrintCenter.Data.Models
{
    public class MaterialConsumptionStream
    {
        public int MaterialConsumptionId { get; set; }
        public MaterialConsumption MaterialConsumption { get; set; }

        public int StreamId { get; set; }
        public Stream Stream { get; set; }
    }
}