namespace PrintCenter.Shared
{
    public class SerialProduction
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double SizeX { get; set; }

        public double SizeY { get; set; }

        public int PackageSize { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public string TechnologyName { get; set; }
    }

    public class SerialProductionsEnvelope : Envelope<SerialProduction>
    {
        public SerialProductionsEnvelope(SerialProduction model, int totalCount) : base(model, totalCount)
        {
        }
    }
}