namespace PrintCenter.Shared
{
    public class CompositeSerialProduction
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int PackageSize { get; set; }
    }

    public class CompositeSerialProductionsEnvelope : Envelope<CompositeSerialProduction> {
        public CompositeSerialProductionsEnvelope(CompositeSerialProduction model, int totalCount) : base(model, totalCount)
        {
        }
    }
}