namespace PrintCenter.Shared
{
    public class MaterialConsumption
    {
        public int Id { get; set; }

        public double Rate { get; set; }

        public int MaterialId { get; set; }
    }

    public class MaterialConsumptionsEnvelope : Envelope<MaterialConsumption>
    {
        public MaterialConsumptionsEnvelope(MaterialConsumption model, int totalCount) : base(model, totalCount)
        {
        }
    }
}