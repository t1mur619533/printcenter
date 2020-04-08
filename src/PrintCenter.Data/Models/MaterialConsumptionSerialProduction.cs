namespace PrintCenter.Data.Models
{
    public class MaterialConsumptionSerialProduction
    {
        public int MaterialConsumptionId { get; set; }
        public MaterialConsumption MaterialConsumption { get; set; }

        public int SerialProductionId { get; set; }
        public SerialProduction SerialProduction { get; set; }
    }
}