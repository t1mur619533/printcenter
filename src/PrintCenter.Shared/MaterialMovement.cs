using System;

namespace PrintCenter.Shared
{
    public class MaterialMovement
    {
        public int Id { get; set; }

        public Material Material { get; set; }

        public DateTime DateTime { get; set; }

        public User User { get; set; }

        public string Type { get; set; }

        public float Count { get; set; }

        public Invoice Invoice { get; set; }
    }

    public class MaterialMovementsEnvelope : Envelope<MaterialMovement>
    {
        public MaterialMovementsEnvelope(MaterialMovement model, int totalCount) : base(model, totalCount)
        {
        }
    }
}