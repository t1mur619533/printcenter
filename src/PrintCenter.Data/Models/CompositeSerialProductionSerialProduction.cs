namespace PrintCenter.Data.Models
{
    public class CompositeSerialProductionSerialProduction
    {
        public int CompositeSerialProductionId { get; set; }

        public CompositeSerialProduction CompositeSerialProduction { get; set; }

        public int SerialProductionId { get; set; }

        public SerialProduction SerialProduction { get; set; }
    }
}